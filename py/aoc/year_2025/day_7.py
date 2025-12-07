from __future__ import annotations
from dataclasses import dataclass

from ..helpers import PuzzleSolver


@dataclass
class Position:
    x: int
    y: int

    def move_down(self):
        return Position(self.x, self.y+1)

    def move_left(self):
        return Position(self.x-1, self.y+1)

    def move_right(self):
        return Position(self.x+1, self.y+1)


@dataclass
class GridItem:
    position: Position
    symbol: str = "."

    def __str__(self) -> str:
        return self.symbol

@dataclass
class TachyonBeam(GridItem):
    symbol: str = "|"

    def hits_splitter(self, line: str):
        return isinstance(line[self.position.x], Splitter)

    def move_down(self) -> TachyonBeam:
        return TachyonBeam(self.position.move_down())

    def move_down_left(self) -> TachyonBeam:
        return TachyonBeam(self.position.move_left())

    def move_down_right(self) -> TachyonBeam:
        return TachyonBeam(self.position.move_right())

    def __repr__(self):
        return f"TachyonBeam({self.position.x},{self.position.y})"

@dataclass
class Splitter(GridItem):
    symbol: str = "^"
    def split(self):
        return [TachyonBeam(self.position.move_left()), TachyonBeam(self.position.move_right())]

@dataclass
class Space(GridItem):
    symbol: str = "."


class Grid:
    def __init__(self, input_lines: list[str]):
        self._grid: list[list[GridItem]] = []

        for y in range(len(input_lines)):
            row = []
            line = input_lines[y]
            for x in range(len(line)):
                char = line[x]
                position = Position(x,y)
                if char == ".":
                    row.append(Space(position))
                elif char == "^":
                    row.append(Splitter(position))
                else:
                    row.append(GridItem(position, symbol=char))
            self._grid.append(row)

    def get_row(self, row: int):
        return self._grid[row]

    def get_row_symbols(self, row: int, symbol: str) -> list[GridItem]:
        return [x for x in self._grid[row] if x.symbol == symbol]

    def display(self):
        border = "#" * len(self._grid[0])
        print(border)
        for row in self._grid:
            print("".join([str(x) for x in row]))
        print(border)

    def add_beams(self, beams: list[TachyonBeam]):
        for beam in beams:
            self._grid[beam.position.y][beam.position.x] = beam

    def __len__(self) -> int:
        return len(self._grid)

class BeamPositions:
    def __init__(self):
        self._positions: list[str] = []

    @property
    def positions(self) -> list[str]:
        return self._positions

    def hash_position(self, beam: TachyonBeam) -> str:
        return f"x{beam.position.x}y{beam.position.y}"

    def record_position(self, beam: TachyonBeam):
        self._positions.append(self.hash_position(beam))

    def contains(self, beam: TachyonBeam):
        return self.hash_position(beam) in self._positions

class PathPositions(BeamPositions):
    pass

class TachyonManifold:
    def __init__(self, grid: Grid, beam_positions: BeamPositions, path_positions: PathPositions):
        self._grid = grid
        self._beam_positions = beam_positions
        self._path_positions = path_positions
        self._splits = 0
        self._paths = 1

    @property
    def splits(self) -> int:
        return self._splits

    @property
    def paths(self) -> int:
        return self._paths

    def emit_beam(self) -> TachyonBeam:
        # Do we need to search downwards in case it is not on the first line? I doubt it...
        x = self._grid.get_row_symbols(0, "S")[0].position.x
        return TachyonBeam(Position(x=x, y=0))

    def get_next_beams_for_beam(self, current_beam: TachyonBeam) -> list[TachyonBeam] | None:
        line_position_y = current_beam.position.y + 1
        if line_position_y >= len(self._grid):
            return None

        line = self._grid.get_row(line_position_y)
        beams = []

        if not current_beam.hits_splitter(line):
            # We head straight down if we do not hit a splitter
            new_beam = current_beam.move_down()
            if not self._path_positions.contains(new_beam):
                self._paths += 1
                self._path_positions.record_position(new_beam)
            return [new_beam]

        # We split left and right if we
        split_beams = [current_beam.move_down_left(), current_beam.move_down_right()]
        for beam in split_beams:
            if self._beam_positions.contains(beam):
                # print("Already contains beam", beam)
                continue
            self._beam_positions.record_position(beam)
            beams.append(beam)
        if len(beams) > 0:
            self._splits += 1
            self._paths += len(beams)
            # print("Split Beam", self._splits, beams)
        return beams

    def get_next_row_of_beams(self, beams: list[TachyonBeam]):
        new_beams = []
        for beam in beams:
            next_beams = self.get_next_beams_for_beam(beam)
            if next_beams is None:
                break
            [new_beams.append(x) for x in next_beams]
        return new_beams

    def process_beam(self):
        beam = self.emit_beam()
        # print("First beam", beam)
        self._beam_positions.record_position(beam)
        beams = [beam]
        y_position = 1
        while y_position < len(self._grid):
            beams = self.get_next_row_of_beams(beams)
            self._grid.add_beams(beams)
            y_position += 1


class Day7(PuzzleSolver):
    def _solve_part_1(self, input_text):
        lines = input_text.split("\n")
        grid = Grid(lines)
        beam_positions = BeamPositions()
        path_positions = PathPositions()
        manifold = TachyonManifold(grid, beam_positions, path_positions)
        manifold.process_beam()
        return str(manifold.splits)

    def _solve_part_2(self, input_text):
        lines = input_text.split("\n")
        grid = Grid(lines)
        grid.display()

        beam_positions = BeamPositions()
        path_positions = PathPositions()
        manifold = TachyonManifold(grid, beam_positions, path_positions)
        manifold.process_beam()
        grid.display()

    
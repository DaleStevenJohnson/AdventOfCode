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
class TachyonBeam:
    position: Position

    def move_down(self) -> TachyonBeam:
        return TachyonBeam(self.position.move_down())

    def move_down_left(self) -> TachyonBeam:
        return TachyonBeam(self.position.move_left())

    def move_down_right(self) -> TachyonBeam:
        return TachyonBeam(self.position.move_right())

    def __repr__(self):
        return f"TachyonBeam({self.position.x},{self.position.y})"


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
    def __init__(self, grid: list[str], beam_positions: BeamPositions, path_positions: PathPositions):
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
        x = self._grid[0].index("S")
        return TachyonBeam(Position(x=x, y=0))

    def next_beams(self, current_beam: TachyonBeam) -> list[TachyonBeam] | None:
        line_position_y = current_beam.position.y + 1
        if line_position_y >= len(self._grid):
            return None

        line = self._grid[line_position_y]
        line_length = len(line)
        start_range = 0
        beams = []
        while start_range != line_length:
            try:
                split = line.index("^", start_range, line_length)
                # print(split)
                if current_beam.position.x != split:
                    start_range = split + 1
                    continue

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
            except ValueError:
                if len(beams) > 0:
                    # print("Exhausted Search")
                    return beams
                # print("Going Down!")
                new_beam = current_beam.move_down()
                if not self._path_positions.contains(new_beam):
                    self._paths += 1
                    self._path_positions.record_position(new_beam)
                return [new_beam]
        return beams


    def print_beams(self, beams: list[TachyonBeam], y_position: int):
        line = self._grid[y_position]
        new_line = ""
        for i in range(len(line)):
            char = line[i]
            for beam in beams:
                if beam.position.x == i:
                    char = "|"
                    break
            new_line += char
        print(new_line)


    def process_beam(self):
        beam = self.emit_beam()
        # print("First beam", beam)
        self._beam_positions.record_position(beam)
        beams = [beam]
        y_position = 1
        while y_position < len(self._grid):
            new_beams = []
            for beam in beams:
                next_beams = self.next_beams(beam)
                if next_beams is None:
                    break
                [new_beams.append(x) for x in next_beams]

            self.print_beams(new_beams, y_position)
            beams = new_beams.copy()
            y_position += 1


class Day7(PuzzleSolver):
    def _solve_part_1(self, input_text):
        lines = input_text.split("\n")
        beam_positions = BeamPositions()
        path_positions = PathPositions()
        manifold = TachyonManifold(lines, beam_positions, path_positions)
        manifold.process_beam()
        return str(manifold.splits)

    def _solve_part_2(self, input_text):
        lines = input_text.split("\n")
        beam_positions = BeamPositions()
        path_positions = PathPositions()
        manifold = TachyonManifold(lines, beam_positions, path_positions)
        manifold.process_beam()
        return str(manifold.paths)

    
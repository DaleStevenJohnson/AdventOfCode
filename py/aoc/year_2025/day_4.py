from __future__ import annotations
from abc import abstractmethod, ABC
from dataclasses import dataclass
from enum import Enum
from typing import Generator

from ..helpers import PuzzleSolver

class ShelfContents(Enum):
    empty = "empty"
    paper = "paper"

@dataclass
class GridPosition:
    x: int
    y: int


    def get_min_position(self, offset: int, limit: int) -> GridPosition:
        if (min_x := self.x - offset) < limit:
            min_x = limit
        if (min_y := self.y - offset) < limit:
            min_y = limit

        return GridPosition(x=min_x, y=min_y)

    def get_max_position(self, offset: int, limit: int) -> GridPosition:
        if (max_x := self.x + offset) > limit:
            max_x = limit
        if (max_y := self.y + offset) > limit:
            max_y = limit

        return GridPosition(x=max_x, y=max_y)

    def __eq__(self, other):
        if not isinstance(other, GridPosition):
            raise NotImplemented

        return self.x == other.x and self.y == other.y

    def __str__(self):
        return f"({self.x}, {self.y})"

@dataclass
class Shelf:
    shelf_contents: ShelfContents
    position: GridPosition

    @classmethod
    def from_str(cls, input_str: str, grid_position: GridPosition):
        contents = ShelfContents.empty
        if input_str == "@":
            contents = ShelfContents.paper
        return cls(
            shelf_contents=contents,
            position=grid_position
        )

    def __str__(self):
        return f"Shelf({self.position.x},{self.position.y}): {self.shelf_contents.value}"


class ShelfContentsDetector(ABC):
    def __init__(self):
        self._count = 0

    @property
    def detected_count(self):
        return self._count

    def detect(self, shelf: Shelf) -> bool:
        if result := self._detect(shelf):
            self._count += 1
        return result

    @abstractmethod
    def _detect(self, shelf: Shelf) -> bool:
        raise NotImplemented

class PaperDetector(ShelfContentsDetector):
    def _detect(self, shelf: Shelf):
        return shelf.shelf_contents == ShelfContents.paper


class Warehouse:
    def __init__(self, shelf_grid: list[list[Shelf]]):
        self._shelf_grid = shelf_grid
        self._removed = 0

    @property
    def removed_count(self) -> int:
        return self._removed

    @classmethod
    def from_input(cls, data: list[list[str]]):
        shelf_grid = []
        for y in range(len(data)):
            shelf_row = []
            for x in range(len(data[y])):
                shelf_row.append(Shelf.from_str(data[y][x], GridPosition(x=x, y=y)))
            shelf_grid.append(shelf_row)
        return cls(shelf_grid=shelf_grid)


    def detect_next(self, detector: ShelfContentsDetector) -> Generator[Shelf, None, None]:
        for y in range(len(self._shelf_grid)):
            for x in range(len(self._shelf_grid[y])):
                shelf = self._shelf_grid[y][x]
                if detector.detect(shelf):
                    yield shelf

    def count_matching_neighbours(self, shelf: Shelf):
        min_position = shelf.position.get_min_position(offset=1, limit=0)
        max_position = shelf.position.get_max_position(offset=1, limit=len(self._shelf_grid[shelf.position.y]) - 1)

        count = 0
        for x in range(min_position.x, max_position.x + 1):
            for y in range(min_position.y, max_position.y + 1):
                other_shelf = self._shelf_grid[y][x]
                if other_shelf.position == shelf.position:
                    continue

                if shelf.shelf_contents.value == other_shelf.shelf_contents.value:
                    count += 1
        return count


    def remove(self, shelf: Shelf):
        self._shelf_grid[shelf.position.y][shelf.position.x] = Shelf(
            position=shelf.position,
            shelf_contents=ShelfContents.empty)
        self._removed += 1



class Day4(PuzzleSolver):
    def _solve_part_1(self, input_text: str) -> str:
        warehouse = Warehouse.from_input([list(row) for row in input_text.split("\n")])
        detector = PaperDetector()
        counts = [
            1
            for shelf in warehouse.detect_next(detector)
            if warehouse.count_matching_neighbours(shelf) < 4
        ]

        return str(sum(counts))



    def _solve_part_2(self, input_text: str) -> str:
        warehouse = Warehouse.from_input([list(row) for row in input_text.split("\n")])
        detector = PaperDetector()
        removed = -1
        while warehouse.removed_count != removed:
            removed = warehouse.removed_count
            for shelf in warehouse.detect_next(detector):
                if warehouse.count_matching_neighbours(shelf) < 4:
                    warehouse.remove(shelf)

        return str(removed)

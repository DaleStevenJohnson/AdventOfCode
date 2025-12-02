
from abc import ABC, abstractmethod


class PuzzleSolver(ABC):
    """Abstract base class for solving an Advent Of Code Puzzle."""

    def __init__(self, day: int):
        self._day = day


    def solve_part_1(self, input_text: str) -> str:
        print(f"Day {self._day}, Part 1")
        answer = self._solve_part_1(input_text)
        print(f"Answer: {answer}")

    def solve_part_2(self, input_text: str) -> str:
        print(f"Day {self._day}, Part 2")
        answer = self._solve_part_2(input_text)
        print(f"Answer: {answer}")

    @abstractmethod
    def _solve_part_1(self, input_text: str) -> str:
        return "No solution yet..."

    @abstractmethod
    def _solve_part_2(self, input_text: str) -> str:
        return "No solution yet..."
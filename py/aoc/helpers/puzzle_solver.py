from abc import ABC, abstractmethod
from time import time


class PuzzleSolver(ABC):
    """Abstract base class for solving an Advent Of Code Puzzle."""

    def __init__(self, day: int):
        self._day = day


    def solve_part_1(self, input_text: str) -> str:
        self._solve(part=1, input_text=input_text)

    def solve_part_2(self, input_text: str) -> str:
        self._solve(part=2, input_text=input_text)

    def _solve(self, part: int, input_text: str):
        print(f"Day {self._day}, Part {part}")
        start = time()
        if part == 1:
            answer = self._solve_part_1(input_text)
        else:
            answer = self._solve_part_2(input_text)
        solution_execution_time = round(time() - start, 2)
        print(f"({solution_execution_time}s) Answer: {answer}")

    @abstractmethod
    def _solve_part_1(self, input_text: str) -> str:
        return "No solution yet..."

    @abstractmethod
    def _solve_part_2(self, input_text: str) -> str:
        return "No solution yet..."
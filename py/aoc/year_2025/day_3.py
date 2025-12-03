from dataclasses import dataclass

from ..helpers import PuzzleSolver


@dataclass
class BatteryBank:
    digits: list[int]

    @property
    def joltage(self) -> int:
        return int(str(self))

    @classmethod
    def from_line(cls, line: str, num_digits: int):
        numbers = [int(x) for x in line]

        position = 0
        digits = []
        for i in range(num_digits):
            end_of_search_range = len(numbers) - num_digits + i + 1
            largest_num = max(numbers[position:end_of_search_range])
            position = numbers.index(largest_num, position, end_of_search_range) + 1
            digits.append(largest_num)


        return cls(
            digits=digits
        )

    def __str__(self):
        return "".join([str(x) for x in self.digits])

    def __repr__(self):
        return f"Joltage: {self}"


class Day3(PuzzleSolver):
    def _solve_part_1(self, input_text: str) -> str:
        battery_banks = [BatteryBank.from_line(x, num_digits=2) for x in input_text.split("\n") if x != ""]
        return str(sum([x.joltage for x in battery_banks]))

    def _solve_part_2(self, input_text: str) -> str:
        battery_banks = [BatteryBank.from_line(x, num_digits=12) for x in input_text.split("\n") if x != ""]
        return str(sum([x.joltage for x in battery_banks]))

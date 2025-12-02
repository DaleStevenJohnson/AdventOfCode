from dataclasses import dataclass

from ..helpers import PuzzleSolver

@dataclass
class RotationCommand:
    amount: int

    @classmethod
    def from_input(cls, input_line: str):
        if len(input_line) < 2:
            return cls(amount=0)

        direction = input_line[0].lower()
        amount = int(input_line[1:])
        if direction == "l":
            amount = -amount

        return cls(
            amount=amount
        )

class Dial:
    def __init__(self):
        self._position = 50
        self._zero_count = 0
        self._upper_bound = 99
        self._lower_bound = 0

    @property
    def zeroes(self):
        return self._zero_count

    def rotate(self, rotation_command: RotationCommand) -> int:
        self._rotate(rotation_command.amount)
        return self._position

    def _rotate(self, amount: int):
        if amount == 0:
            return

        increment = 1
        if amount < 0:
            increment = -1

        for i in range(abs(amount)):
            self._position += increment
            if self._position < self._lower_bound:
                self._position = self._upper_bound
            elif self._position > self._upper_bound:
                self._position = self._lower_bound

            if self._position == 0:
                self._zero_count += 1


class Day1(PuzzleSolver):
    def _solve_part_1(self, input_text: str) -> str:
        commands = [RotationCommand.from_input(x) for x in input_text.split("\n") if len(x) > 1]
        dial = Dial()
        positions = [y for y in [dial.rotate(x) for x in commands]]
        #print(positions)
        return str(sum([1 for x in positions if x == 0]))

    def _solve_part_2(self, input_text: str) -> str:
        commands = [RotationCommand.from_input(x) for x in input_text.split("\n") if len(x) > 1]
        dial = Dial()
        positions = [y for y in [dial.rotate(x) for x in commands]]
        #print(positions)
        return str(dial.zeroes)
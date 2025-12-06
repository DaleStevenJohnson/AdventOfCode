from math import prod

from ..helpers import PuzzleSolver

class CephalopodNumberReader:
    def __init__(self):
        pass

    def convert(self, lines: list[str]) -> list[list[int]]:
        """
        Position of the numbers is important in Cephalopod number system, so we expect a
        string full of whitespace and numbers rather than cold hard ints.
        """
        position = max([len(x) for x in lines]) - 1
        results: list[list[int]] = []
        column: list[int] = []

        # Work backwards through the positions
        while position >= 0:
            new_number = ""

            for number in lines:
                try:
                    new_number += number[position].replace(" ", "")
                except IndexError:
                    # It's unlikely this will ever be hit, but hey ho!
                    continue

            # Each operation is separated by a column of whitespace
            if new_number != "":
                column.append(int(new_number))

            # We can detect that whitespace to know when we have moved on to a new column
            if new_number == "" or position == 0:
                results.append(column.copy())
                column = []

            position -= 1

        # We are reading right to left, but appending columns left to right, so we need to reverse before returning
        results.reverse()
        return results


class CephalopodCalculator:
    def __init__(self, operators: list[str]):
        self._operators = operators

    def calculate(self, numbers: list[int], column: int) -> int:
        operation = self._operators[column]
        if operation == "*":
            return prod(numbers)
        else:
            return sum(numbers)




class Day6(PuzzleSolver):
    def _solve_part_1(self, input_text):
        lines = [[y for y in x.split(" ") if y != ""] for x in input_text.split("\n") if x != ""]

        number_len = len(lines[0])

        operators = lines.pop()
        calculator = CephalopodCalculator(operators)
        results: list[int] = []
        for i in range(number_len):
            numbers: list[int] = [int(line[i]) for line in lines]
            results.append(calculator.calculate(numbers, i))

        return str(sum(results))

    def _solve_part_2(self, input_text):
        lines = [x for x in input_text.split("\n") if x != ""]
        calculator = CephalopodCalculator([x for x in lines.pop().split(" ") if x != ""])
        number_reader = CephalopodNumberReader()
        results: list[int] = []
        numbers = number_reader.convert(lines)

        for i in range(len(numbers)):
            results.append(calculator.calculate(numbers[i], i))

        return str(sum(results))

    
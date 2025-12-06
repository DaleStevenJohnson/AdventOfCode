from math import prod

from ..helpers import PuzzleSolver



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
        return super()._solve_part_2(input_text)

    
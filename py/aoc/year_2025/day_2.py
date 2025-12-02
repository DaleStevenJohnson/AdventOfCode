from abc import ABC, abstractmethod

from ..helpers import PuzzleSolver

class Validator(ABC):
    @abstractmethod
    def is_valid(self, number: int) -> bool:
        raise NotImplemented


class SimpleValidator(Validator):
    def is_valid(self, number: int) -> bool:
        str_num = str(number)
        str_len = len(str_num)
        if str_len % 2 != 0:
            return True

        halfway = int(str_len / 2)

        left = str_num[:halfway]
        right = str_num[halfway:]

        return left != right

class AdvancedValidator(Validator):
    def is_valid(self, number: int) -> bool:
        str_num = str(number)
        str_len = len(str_num)

        halfway = int(str_len / 2)
        left = str_num[:halfway]

        # print("Checking Subs...")
        for i in range(1, len(left) + 1):
            if str_len % i != 0:
                continue

            multiples = int(str_len / i)
            sub = left[:i] * multiples
            # print(f"Multiple: {multiples} ({str_len}/{i}), Sub: {sub}")
            if sub == str_num:
                # print(f"{number} NOT valid")
                return False
        # print(f"{number} is valid")
        return True
   

class ProductIdRange:
    def __init__(self, start: int, end: int):
        self._start = start
        self._end = end
        self._sum = 0
        self._total = 0

    def validate(self, validator: Validator) -> int:
        # print(self)
        for i in range(self._start, self._end + 1):
            if validator.is_valid(i):
                # print(f"Valid Id: {i}")
                continue

            self._sum += i
            # print(f"Id ({i}) - Sum: {self._sum}")
            self._total += 1
        return self._sum

    def __str__(self):
        return f"Product Id Range: {self._start}-{self._end}"

    def __repr__(self):
        return f"{self._start}-{self._end}"



class Day2(PuzzleSolver):
    def _solve_part_1(self, input_text: str) -> str:
        validator = SimpleValidator()
        ranges = [ProductIdRange(*map(int, y.split("-"))) for y in [x for x in input_text.split(",") if x != ""]]
        result = sum([x.validate(validator) for x in ranges])
        return str(result)


    def _solve_part_2(self, input_text: str) -> str:
        validator = AdvancedValidator()
        ranges = [ProductIdRange(*map(int, y.split("-"))) for y in [x for x in input_text.split(",") if x != ""]]
        result = sum([x.validate(validator) for x in ranges])
        return str(result)
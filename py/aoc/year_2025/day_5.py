from __future__ import annotations
from abc import ABC, abstractmethod
from dataclasses import dataclass
from typing import Any

from ..helpers import PuzzleSolver


@dataclass
class IngredientIdRange:
    start: int
    end: int

    def is_in_range(self, x: int) -> bool:
        return self.start <= x <= self.end

    def overlaps(self, other: IngredientIdRange) -> bool:
        return self.start <= other.start <= self.end or self.start <= other.end <= self.end

    @classmethod
    def from_intersection(cls, range_1: IngredientIdRange, range_2: IngredientIdRange) -> IngredientIdRange:
        start = min(range_1.start, range_2.start)
        end = max(range_1.end, range_2.end)
        return cls(start, end)

    @classmethod
    def from_input(cls, input_str: str):
        start, end = map(int, input_str.split("-"))
        return cls(start, end)

    def __gt__(self, other: Any) -> bool:
        if not isinstance(other, IngredientIdRange):
            return NotImplemented
        return self.start > other.start

    def __lt__(self, other: Any) -> bool:
        if not isinstance(other, IngredientIdRange):
            return NotImplemented
        return self.start < other.start

    def __len__(self):
        return self.end - self.start + 1

    def __iter__(self):
        for i in range(self.start, self.end + 1):
            yield i

    def __str__(self):
        return f"IngredientIdRange: {self.start} - {self.end}"


class InventoryCompressor(ABC):
    @abstractmethod
    def compress(self, ingredient_id_ranges: list[IngredientIdRange]) -> list[IngredientIdRange]:
        raise NotImplemented


class StartIdCompressor(InventoryCompressor):
    def compress(self, ingredient_id_ranges: list[IngredientIdRange]) -> list[IngredientIdRange]:
        ranges: dict[int, IngredientIdRange] = {}
        compressed = 0
        for ingredient_range in ingredient_id_ranges:
            if ingredient_range.start in ranges:
                compressed += 1
                existing_range = ranges[ingredient_range.start]
                if existing_range.end < ingredient_range.end:
                    ranges[ingredient_range.start] = ingredient_range
            else:
                ranges[ingredient_range.start] = ingredient_range
        print(f"Compressed {compressed} ranges")
        return list(ranges.values())


class EndIdCompressor(InventoryCompressor):
    def compress(self, ingredient_id_ranges: list[IngredientIdRange]) -> list[IngredientIdRange]:
        ranges: dict[int, IngredientIdRange] = {}
        compressed = 0
        for ingredient_range in ingredient_id_ranges:
            if ingredient_range.end in ranges:
                compressed += 1
                existing_range = ranges[ingredient_range.end]
                if existing_range.start > ingredient_range.start:
                    ranges[ingredient_range.start] = ingredient_range
            else:
                ranges[ingredient_range.start] = ingredient_range
        print(f"compressed {compressed} ranges")
        return list(ranges.values())


class CompositeCompressor(InventoryCompressor):
    def __init__(self, compressors: list[InventoryCompressor]):
        self._compressors = compressors

    def compress(self, ingredient_id_ranges: list[IngredientIdRange]) -> list[IngredientIdRange]:
        for compressor in self._compressors:
            ingredient_id_ranges = compressor.compress(ingredient_id_ranges)
        return ingredient_id_ranges


class IngredientIdRangeMerger:
    def merge(self, input_ranges: list[IngredientIdRange]) -> list[IngredientIdRange]:
        ingredient_id_ranges = input_ranges.copy()
        ingredient_id_ranges.sort(key=lambda x: x.start)

        adjusted_ingredient_ranges = []

        while len(ingredient_id_ranges) > 0:
            ingredient_range = ingredient_id_ranges.pop(0)
            overlapped = False

            for i in range(len(ingredient_id_ranges) - 1, -1, -1):
                if ingredient_range.overlaps(ingredient_id_ranges[i]):
                    overlapped = True
                    other_ingredient_range = ingredient_id_ranges.pop(i)
                    adjusted_ingredient_ranges.append(IngredientIdRange.from_intersection(ingredient_range, other_ingredient_range))

            if not overlapped:
                adjusted_ingredient_ranges.append(ingredient_range)

        return adjusted_ingredient_ranges


class Inventory:
    def __init__(self, ingredient_ranges: list[IngredientIdRange]):
        self._ingredient_ranges = ingredient_ranges

    def compress(self, compressor: InventoryCompressor):
        self._ingredient_ranges = compressor.compress(self._ingredient_ranges)

    def merge_ranges(self, range_merger: IngredientIdRangeMerger):
        ingredient_ranges: list[IngredientIdRange] = []
        new_ingredient_ranges: list[IngredientIdRange] = self._ingredient_ranges.copy()

        ingredient_range_length = len(ingredient_ranges)
        new_ingredient_range_length = len(new_ingredient_ranges)

        while ingredient_range_length != new_ingredient_range_length:
            ingredient_ranges = new_ingredient_ranges
            new_ingredient_ranges = range_merger.merge(ingredient_ranges)

            ingredient_range_length = len(ingredient_ranges)
            new_ingredient_range_length = len(new_ingredient_ranges)

        return new_ingredient_ranges

    @classmethod
    def from_input(cls, input_str: str):
        ingredient_ranges = [IngredientIdRange.from_input(x) for x in input_str.split("\n") if "-" in x]
        return cls(ingredient_ranges)


class Day5(PuzzleSolver):
    def _solve_part_1(self, input_text: str) -> str:
        ingredient_ranges = [IngredientIdRange.from_input(x) for x in input_text.split("\n") if "-" in x]
        available_ingredient_ids = [int(x) for x in input_text.split("\n") if "-" not in x and x != ""]

        fresh_count = 0
        for available_ingredient_id in available_ingredient_ids:
            for ingredient_range in ingredient_ranges:
                if ingredient_range.is_in_range(available_ingredient_id):
                    fresh_count += 1
                    break

        return str(fresh_count)


    def _solve_part_2(self, input_text: str) -> str:
        inventory = Inventory.from_input(input_text)
        compressor = CompositeCompressor([
            StartIdCompressor(),
            EndIdCompressor(),
        ])
        merger = IngredientIdRangeMerger()

        inventory.compress(compressor)

        result = sum([len(x) for x in inventory.merge_ranges(merger)])
        return str(result)
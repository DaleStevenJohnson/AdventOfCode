from importlib import import_module
from types import ModuleType
from typing import Type

from .puzzle_solver import PuzzleSolver


class PuzzleSolverFactory:
    def __init__(self, year: int):
        self._year = year

    def _get_module_name(self, day: int):
         return f"aoc.year_{self._year}.day_{day}"

    def create(self, day: int) -> PuzzleSolver:
        module_name = self._get_module_name(day)
        class_name = f"Day{day}"

        try:
            # 3. Dynamic Import
            # This searches sys.modules first, then finds/loads the file.
            module: ModuleType = import_module(module_name)

            # 4. Reflection to get the class
            cls: Type[PuzzleSolver] = getattr(module, class_name)

            return cls(day)

        except ModuleNotFoundError:
            print(f"Day {day} has not been implemented yet (File {module_name} missing).")
            exit(1)
        except AttributeError:
            print(f"BLAH Did not find class {class_name} in {module_name}.")
            exit(1)
        except ImportError:
            print(f"Did not find class {class_name} in {module_name}.")
            exit(1)
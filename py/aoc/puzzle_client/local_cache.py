from os import getenv
from pathlib import Path


class LocalCache:
    """Cache for storing puzzle data taken from the API locally, so as not to need an internet connection when running puzzles."""
    def __init__(self, year: int):
        self._path = Path(getenv('APPDATA')).joinpath("AdventOfCode", str(year))
        self._path.mkdir(parents=True, exist_ok=True)

    def get_filename(self, day: int):
        return f"Day-{day}.txt"

    def persist(self, day: int, data: str) -> bool:
        try:
            self._path.joinpath(self.get_filename(day)).write_text(data)
            return True
        except FileNotFoundError:
            return False

    def get(self, day: int) -> str | None:
        if self._path.joinpath(self.get_filename(day)).exists():
            with self._path.joinpath(self.get_filename(day)).open() as f:
                return f.read()
        return None
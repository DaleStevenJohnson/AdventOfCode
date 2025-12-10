from requests import get
from .local_cache import LocalCache

class PuzzleClient:
    """Interacts with the Advent Of Code API to get puzzle input"""
    def __init__(self, year: int, session_id: str, local_cache: LocalCache, ):
        self._session_id = session_id
        self._year = year
        self._local_cache = local_cache

    @property
    def headers(self):
        return {
            "Cookie": f"session={self._session_id}"
        }

    def get_input(self, day: int, refresh: bool = False) -> str:
        if not refresh and (cached_data := self._local_cache.get(day)) is not None:
            return cached_data

        print("Calling API for puzzle input")
        url = f"https://adventofcode.com/{self._year}/day/{day}/input"
        response = get(url, headers=self.headers)
        if response.status_code == 404:
            print(f"Puzzle Not Available for Year {self._year} Day {day} ")
            exit(1)
        response.raise_for_status()

        self._local_cache.persist(day, response.text)
        return response.text

    def get_description(self, day: int):
        url = f"https://adventofcode.com/{self._year}/day/{day}"
        response = get(url, headers=self.headers)
        response.raise_for_status()
        return response.json()
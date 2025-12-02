from .puzzle_client import PuzzleClient
from .local_cache import LocalCache


class ClientFactory:
    """Creates dependencies for a standard client so you don't have to!"""
    def __init__(self, year: int, session_id: str):
        self._year = year
        self._session_id = session_id

    def create(self) -> PuzzleClient:
        local_cache = LocalCache(year=self._year)
        return PuzzleClient(year=self._year, session_id=self._session_id, local_cache=local_cache)
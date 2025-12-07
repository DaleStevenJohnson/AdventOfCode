from click import argument, command, option
from pygments.lexer import default

from .puzzle_client import ClientFactory
from .helpers import PuzzleSolverFactory

@command()
@argument("session-id", type=str)
@argument("year", type=int)
@argument("day", type=int)
@option("--test-data", type=str, default=None)
@option("--refresh", is_flag=True, default=False, type=bool)
def main(session_id: str, year: int, day: int, refresh: bool, test_data: str | None):
    ## Get Input
    input_text = test_data
    if test_data is None:
        client_factory = ClientFactory(year=year, session_id=session_id)
        client = client_factory.create()
        input_text = client.get_input(day=day, refresh=refresh)

    ## Get Solver
    puzzle_factory = PuzzleSolverFactory(year=year)
    solver = puzzle_factory.create(day)

    ## Solve Part 1
    solver.solve_part_1(input_text)

    ## Solve Part 2
    solver.solve_part_2(input_text)

if __name__ == '__main__':
    main()
from pytest import mark, param

from aoc.year_2025.day_1 import Dial, RotationCommand



@mark.parametrize(
    ["rotation_amount", "result"],
    (
        param(50, 0, id="50"),
        param(100, 50, id="100"),
        param(-50, 0, id="-50"),
        param(-100, 50, id="-100"),
    )
)
def test_dial(rotation_amount: int, result: int):
    dial = Dial()
    command = RotationCommand(amount=rotation_amount)
    position = dial.rotate(command)
    assert position == result

@mark.parametrize(
    ["rotation_command", "result"],
    (
param("R50", 0, id="R50"),
param("R100", 50, id="R100"),
param("L50", 0, id="L50"),
param("L100", 50, id="L100"),
param("R1", 51, id="R1"),
param("L1", 49, id="L1"),
    )
)
def test_rotation_command_with_dial(rotation_command: str, result: int):
    dial = Dial()
    command = RotationCommand.from_input(rotation_command)
    position = dial.rotate(command)
    assert position == result

@mark.parametrize(
    ["rotation_amount", "result"],
    (
        param(50, 1, id="50"),
        param(100, 1, id="100"),
        param(-50, 1, id="-50"),
        param(-100, 1, id="-100"),
        param(255, 3, id="255"),
        param(-255, 3, id="-255"),
    )
)
def test_zeroes(rotation_amount: int, result: int):
    dial = Dial()
    command = RotationCommand(amount=rotation_amount)
    dial.rotate(command)
    assert dial.zeroes == result

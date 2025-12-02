using System;
using System.Collections.Generic;
using ChallengeSolutions.Helpers;

namespace ChallengeSolutions.Y2021
{
    public class Y2021D2
    {
        public static string SolvePart1(bool test = false)
        {
            var puzzleInput = MyIO.ReadStringsFromFile(2021, 2, 1, test);
            var horizontalPosition = 0;
            var depth = 0;
            for (int i = 0; i < puzzleInput.Count; i++)
            {
                var instruction = puzzleInput[i].Split(" ");
                var direction = instruction[0];
                var amount = Int32.Parse(instruction[1]);
                if (direction == "forward")
                {
                    horizontalPosition += amount;
                }
                else
                {
                    if (direction == "up")
                    {
                        amount = -amount;
                    }
                    depth += amount;
                }

            }
            var result = depth * horizontalPosition;
            return $"{result}";
        }

        public static string SolvePart2(bool test = false)
        {
            var puzzleInput = MyIO.ReadStringsFromFile(2021, 2, 1, test);
            var horizontalPosition = 0;
            var depth = 0;
            var aim = 0;
            for (int i = 0; i < puzzleInput.Count; i++)
            {
                var instruction = puzzleInput[i].Split(" ");
                var direction = instruction[0];
                var amount = Int32.Parse(instruction[1]);
                if (direction == "forward")
                {
                    horizontalPosition += amount;
                    depth += aim * amount;
                }
                else
                {
                    if (direction == "up")
                    {
                        amount = -amount;
                    }
                    aim += amount;
                }

            }
            var result = depth * horizontalPosition;
            return $"{result}";
        }

        
    }
}

using System;
using System.Collections.Generic;
using ChallengeSolutions.Helpers;

namespace ChallengeSolutions.Y2021
{
    public class Y2021D1
    {
        public static string SolvePart1(bool test = false)
        {
            MyIO.CreateEmptyDataFiles(2021);

            var puzzleInput = MyIO.ReadIntsFromFile(2021, 1, 1, test);
            var depthMeasurementIncreasedCount = 0;
            for (int i = 0; i < puzzleInput.Count - 1; i++)
            {
                var thisMeasurement = puzzleInput[i];
                var nextMeasurement = puzzleInput[i + 1];
                if (nextMeasurement > thisMeasurement)
                    depthMeasurementIncreasedCount++;
            }
            return $"{depthMeasurementIncreasedCount}";
        }

        public static string SolvePart2(bool test = false)
        {
            var puzzleInput = MyIO.ReadIntsFromFile(2021, 1, 1, test);
            var slidingWindowIncreasedCount = 0;
            for (int i = 0; i < puzzleInput.Count - 3; i++)
            {
                var thisWindow = GetSumOfNextThreePositions(puzzleInput, i);
                var nextWindow = GetSumOfNextThreePositions(puzzleInput, i + 1);
                if (nextWindow > thisWindow)
                    slidingWindowIncreasedCount++;
            }
            
            return $"{slidingWindowIncreasedCount}";
        }

        public static int GetSumOfNextThreePositions(List<int> list, int index)
        {
            var sum = 0;
            for (int i = index; i < index + 3; i++)
            {
                sum += list[i];
            }
            return sum;
        }
    }
}

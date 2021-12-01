using System;
using System.Collections.Generic;
using ChallengeSolutions.Helpers;

namespace ChallengeSolutions.Y2021
{
    public class Y2021D1
    {
        public static string SolveDay1(bool test = false)
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
    }
}

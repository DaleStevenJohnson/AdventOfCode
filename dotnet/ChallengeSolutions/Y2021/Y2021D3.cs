using System;
using System.Collections.Generic;
using System.Linq;
using ChallengeSolutions.Helpers;

namespace ChallengeSolutions.Y2021
{
    public class Y2021D3
    {
        public static int CountBitsInPositionI(List<string> inputList, int i, out int zeros)
        {
            var ones = 0;
            zeros = 0;
            foreach (var code in inputList)
            {
                var digit = code[i];
                if (digit == '1')
                    ones++;
                else
                    zeros++;
            }
            return ones;
        }

        public static string SolvePart1(bool test = false)
        {
            var puzzleInput = MyIO.ReadStringsFromFile(2021, 3, 1, test);
            var result = 0;
            var gamma = "";
            var epsilon = "";
            if (puzzleInput.Any())
            {
                for (int i = 0; i < puzzleInput[0].Length; i++)
                {
                    var ones = CountBitsInPositionI(puzzleInput, i, out var zeros);
                    gamma += ones > zeros ? "1" : "0";
                    epsilon += ones > zeros ? "0" : "1";
                }
            }
            result = BinaryFunctions.ConvertStringToInt(gamma) * BinaryFunctions.ConvertStringToInt(epsilon);
            return $"{result}";
        }

        public static string SolvePart2(bool test = false)
        {
            var puzzleInput = MyIO.ReadStringsFromFile(2021, 3, 1, test);
            var oxygenCodes = new List<string>(puzzleInput);
            var co2Codes = new List<string>(puzzleInput);
            var result = 0;
            var oxygenGeneratorRating = 0;
            var co2ScrubberRating = 0;

            for (int i = 0; i < puzzleInput[0].Length; i++)
            {
                if (oxygenCodes.Count > 1)
                {
                    var oxygenGeneratorRatingCriteria = GetOxygenGeneratorRatingCriteria(oxygenCodes, i);
                    oxygenCodes = FilterListsByBitAtPositionI(oxygenCodes, i, oxygenGeneratorRatingCriteria);
                }

                if (co2Codes.Count > 1)
                {
                    var co2ScrubberRatingCriteria = GetCO2ScrubberRatingCriteria(co2Codes, i);
                    co2Codes = FilterListsByBitAtPositionI(co2Codes, i, co2ScrubberRatingCriteria);
                }
            }

            oxygenGeneratorRating = BinaryFunctions.ConvertStringToInt(oxygenCodes[0]);
            co2ScrubberRating = BinaryFunctions.ConvertStringToInt(co2Codes[0]);
            result = oxygenGeneratorRating * co2ScrubberRating;

            return $"{result}";
        }

        private static char GetOxygenGeneratorRatingCriteria(List<string> inputList, int i)
        {
            var ones = CountBitsInPositionI(inputList, i, out var zeros);
            return zeros > ones ? '0' : '1';
        }

        private static char GetCO2ScrubberRatingCriteria(List<string> inputList, int i)
        {
            var ones = CountBitsInPositionI(inputList, i, out var zeros);
            return zeros > ones ? '1' : '0';
        }


        public static List<string> FilterListsByBitAtPositionI(List<string> inputList, int position, char criteria = '1')
        {
            var newList = new List<string>();
            foreach (string binaryCode in inputList)
            {
                if (binaryCode[position] == criteria)
                    newList.Add(binaryCode);
            }
            return newList;
        }

       
        
    }
}

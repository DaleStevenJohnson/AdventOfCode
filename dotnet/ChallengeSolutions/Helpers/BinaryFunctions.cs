using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeSolutions.Helpers
{
    public static class BinaryFunctions
    {
        public static int ConvertStringToInt(string binaryString)
        {
            double position = 0;
            int result = 0;
            for (int i = binaryString.Length-1; i > -1; i--)
            {
                int placeValue = (int) Math.Pow(2, position);
                if (binaryString[i] == '1')
                {
                    result += placeValue;
                }
                position++;
            }
            return result;
        }
    }
}

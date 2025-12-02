using System.Collections.Generic;
using System.Text;
using ChallengeSolutions.Abstractions;
using ChallengeSolutions.Helpers;

namespace ChallengeSolutions.Y2022
{
  
    public class Y2022D4 : ISolution
    {
       
        public List<string> TestData { get; set; }
        public List<string> PuzzleData { get; set; }
        public Y2022D4()
        {
            TestData = MyIO.ReadStringsFromFile(2022, 4, 1, true);
            PuzzleData = MyIO.ReadStringsFromFile(2022, 4, 1, false);
        }

        public bool thisRangeContainsThatRange(string[] thisRange, string[] thatRange)
        {
            var range1 = MyIO.ConvertStringArrayToIntArray(thisRange);
            var range2 = MyIO.ConvertStringArrayToIntArray(thatRange);
            return range1[0] >= range2[0] && range1[1] <= range2[1];
        }

        public string SolvePart1(List<string> data)
        {
            var count = 0;
            foreach (var item in data)
            {
                var pair = item.Split(",");
                var range1 = pair[0].Split("-");
                var range2 = pair[1].Split("-");
                if (thisRangeContainsThatRange(range1, range2) || thisRangeContainsThatRange(range2, range1))
                    count++;

            }
            return count.ToString();
        }

        public bool thisRangeOverlapsThatRange(string[] thisRange, string[] thatRange)
        {
            var range1 = MyIO.ConvertStringArrayToIntArray(thisRange);
            var range2 = MyIO.ConvertStringArrayToIntArray(thatRange);
            var containsCompletely = thisRangeContainsThatRange(thisRange, thatRange);
            var overlapsAtEnd = range1[1] >= range2[0] && range1[0] <= range2[1];
            var overlapsAtStart = range1[0] <= range2[1] && range1[1] >= range2[0];
            return containsCompletely || overlapsAtEnd || overlapsAtStart;
        }

        public string SolvePart2(List<string> data)
        {
            var count = 0;
            foreach (var item in data)
            {
                var pair = item.Split(",");
                var range1 = pair[0].Split("-");
                var range2 = pair[1].Split("-");
                if (thisRangeOverlapsThatRange(range1, range2))
                    count++;

            }
            return count.ToString();
        }
    }
}
 
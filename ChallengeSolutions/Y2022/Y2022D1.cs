using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeSolutions.Abstractions;
using ChallengeSolutions.Helpers;

namespace ChallengeSolutions.Y2022
{
    public class Y2022D1 : ISolution
    {
        public List<string> TestData { get; set; }
        public List<string> PuzzleData { get; set; }
        public Y2022D1()
        {
            TestData = MyIO.ReadStringsFromFile(2022, 1, 1, true);
            PuzzleData = MyIO.ReadStringsFromFile(2022, 1, 1, false);
        }
        public string SolvePart1(List<string> data)
        {
            List<int> elfTotals = SumElfCalories(data);
            
            
            return elfTotals.Max().ToString();
        }

        public List<int> SumElfCalories(List<string> data)
        {
            List<int> totals = new List<int>();
            var calories = 0;
            foreach (var item in data)
            {
                if (item == "")
                {
                    totals.Add(calories);
                    calories = 0;
                }
                int number;
                bool success = int.TryParse(item, out number);
                if (success)
                    calories += number;
            }
            if (calories > 0) totals.Add(calories);
            return totals;
        }

        public string SolvePart2(List<string> data)
        {
            var elfTotals = SumElfCalories(data);
            elfTotals.Sort();
            var total = 0;
            for (int i = 1; i <= 3; i++)
            {
                var index = elfTotals.Count - i;
                total += elfTotals[index];
            }
            return total.ToString();
        }
    }
}

using System.Collections.Generic;
using System.Text;
using ChallengeSolutions.Abstractions;
using ChallengeSolutions.Helpers;

namespace ChallengeSolutions.Y2022
{
  
    public class Y2022D3 : ISolution
    {
       
        public List<string> TestData { get; set; }
        public List<string> PuzzleData { get; set; }
        public Y2022D3()
        {
            TestData = MyIO.ReadStringsFromFile(2022, 3, 1, true);
            PuzzleData = MyIO.ReadStringsFromFile(2022, 3, 1, false);
        }

        public string SearchBackpackForDuplicates(string backpack)
        {
            int halfway = backpack.Length / 2;
            var compartment1 = backpack.Substring(0, halfway);
            var compartment2 = backpack.Substring(halfway);
            var duplicates = string.Empty;
            foreach (var item in compartment1)
            {
                if (compartment2.Contains(item) && !duplicates.Contains(item))
                {
                    duplicates += item;
                }
            }
            return duplicates;
        }

        public int GetPriorityScore(string items) 
        {
            var total = 0;
            for (int i = 0; i < items.Length; i++)
            {
                int ascii = (int)items[i];
                if (ascii + 4 > 100)
                {
                    total += ascii + 4 - 100; // I know I could do -96!
                }
                else
                {
                    total += ascii - 38;
                }

            }
            return total;
        }

        public string SolvePart1(List<string> data)
        {
           var total = 0;
           foreach (var backpack in data) 
            {
                var duplicates = SearchBackpackForDuplicates(backpack);
                var score =  GetPriorityScore(duplicates);
                total += score;
            }
           return total.ToString();
        }

        public static string? SearchBackpacksForBadge(string backpack1, string backpack2, string backpack3)
        {
            foreach (var letter in backpack1)
            {
                if (backpack2.Contains(letter) && backpack3.Contains(letter)) return letter.ToString();
            }
            return null;
        }

        public string SolvePart2(List<string> data)
        {
            var total = 0;
            for (int i = 0; i < data.Count; i+=3)
            {
                var backpack1 = data[i];
                var backpack2 = data[i+1];
                var backpack3 = data[i+2];
                var badge = SearchBackpacksForBadge(backpack1, backpack2, backpack3);
                var score = GetPriorityScore(badge);
                total += score;
            }
            return total.ToString();
        }
    }
}

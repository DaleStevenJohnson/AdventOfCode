using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ChallengeSolutions.Helpers
{
    internal class MyIO
    {
        private static string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PuzzleInputs");
        internal static List<string> ReadFile(int year, int day=1, int part=1)
        {
            var dataFile = GetDataFileName(year, day, part);
            string[] lines = System.IO.File.ReadAllLines(Path.Combine(_filePath, dataFile);
            return lines.ToList();
        }

        private static string GetDataFileName(int year, int day, int part)
        {
            return $"Y{year}D{day}P{part}.txt";
        }
    }
}

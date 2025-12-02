using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeSolutions.Abstractions
{
    public interface ISolution
    {
        public List<string> TestData { get; set; }
        public List<string> PuzzleData { get; set; }
        public string SolvePart1(List<string> data);
        public string SolvePart2(List<string> data);
    }
}

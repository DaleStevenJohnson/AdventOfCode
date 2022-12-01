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
        public string SolveDay1(List<string> data);
        public string SolveDay2(List<string> data);
    }
}

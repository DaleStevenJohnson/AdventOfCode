using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeSolutions.Abstractions;
using ChallengeSolutions.Helpers;

namespace ChallengeSolutions.Y2022
{
    public enum RPS
    { 
        Rock = 1,
        Paper = 2,
        Scissors = 3,
    }
    public class Y2022D2 : ISolution
    {
        private Dictionary<string, RPS> TurnLookup = new Dictionary<string, RPS>()
        {
            { "A", RPS.Rock},
            { "B", RPS.Paper},
            { "C", RPS.Scissors},
            { "X", RPS.Rock},
            { "Y", RPS.Paper},
            { "Z", RPS.Scissors},
        };

        public List<string> TestData { get; set; }
        public List<string> PuzzleData { get; set; }
        public Y2022D2()
        {
            TestData = MyIO.ReadStringsFromFile(2022, 2, 1, true);
            PuzzleData = MyIO.ReadStringsFromFile(2022, 2, 1, false);
        }
        public string SolvePart1(List<string> data)
        {
            var score = 0;
            foreach (var move in data)
            {
                var turn = move.Split(" ");
                var opponent = TurnLookup[turn[0]];
                var myself = TurnLookup[turn[1]];
                score += GetScore(myself, opponent);
                score += GetRPSValue(myself);
            }
            return score.ToString();
        }

        public int GetRPSValue(RPS choice)
        { 
            switch (choice) 
            { 
                case RPS.Rock: return 1;
                case RPS.Paper: return 2;
                case RPS.Scissors: return 3;
                default: return 0;
            }
        }

        public int GetScore(RPS myself, RPS opponent)
        {
            if (opponent == myself)
                return 3;
            else if ((opponent == RPS.Rock && myself == RPS.Paper) || (opponent == RPS.Paper && myself == RPS.Scissors) || (opponent == RPS.Scissors && myself == RPS.Rock))
                return 6;
            else
                return 0;
        }
        public RPS GetWinningMove(RPS opponent)
        {
            switch (opponent)
            {
                case RPS.Rock: return RPS.Paper;
                case RPS.Paper: return RPS.Scissors;
                case RPS.Scissors: return RPS.Rock;
                default: return RPS.Paper;
            }
        }
        public RPS GetLosingMove(RPS opponent)
        {
            switch (opponent)
            {
                case RPS.Rock: return RPS.Scissors;
                case RPS.Paper: return RPS.Rock;
                case RPS.Scissors: return RPS.Paper;
                default: return RPS.Paper;
            }
        }

        public RPS GetTurnAction(string turn, RPS opponent)
        {
            if (turn == "Y") return opponent;
            else if (turn == "Z") return GetWinningMove(opponent);
            else return GetLosingMove(opponent);
        }
        public string SolvePart2(List<string> data)
        {
            var score = 0;
            foreach (var move in data)
            {
                var turn = move.Split(" ");
                var opponent = TurnLookup[turn[0]];
                var myself = GetTurnAction(turn[1], opponent);
                score += GetScore(myself, opponent);
                score += GetRPSValue(myself);
            }
            return score.ToString();
        }
    }
}

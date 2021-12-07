using System;
using System.Collections.Generic;
using System.Linq;
using ChallengeSolutions.Helpers;

namespace ChallengeSolutions.Y2021
{
    public class Y2021D4
    {
       

        public static string SolvePart1(bool test = false)
        {
            var puzzleInput = MyIO.ReadStringsFromFile(2021, 4, 1, test);
            var result = 0;
            var numbersToCall = puzzleInput[0].Split(",");
            List<Board> boards = GetBoards(puzzleInput);
            foreach (var numberToCall in numbersToCall)
            {
                foreach (var board in boards)
                {
                    if (board.FindOnBoard(numberToCall))
                        result = int.Parse(numberToCall) * board.UnmarkedSum;
                        return $"{result}";
                }
            }
            
            return $"Not Found";
        }

        private static List<Board> GetBoards(List<string> inputList)
        {
            List<Board> boards = new List<Board>();
            int index = 1;
            int boardRows = 5;
            do
            {
                if (inputList[index] == "")
                {
                    boards.Add(new Board(inputList.GetRange(index + 1, boardRows)));
                    index += boardRows;
                }
                else
                    index += 1;
            } while (index < inputList.Count);
            return boards;
        }

        public static string SolvePart2(bool test = false)
        {
            var puzzleInput = MyIO.ReadStringsFromFile(2021, 4, 1, test);
            var result = 0;
            
            return $"{result}";
        }

        

       
        
    }

    internal class Board
    {
        private readonly string _marked = "MARKED";
        internal Board(List<string> inputData)
        {
            State = new string[inputData.Count][];
            for (int i = 0; i < inputData.Count; i++)
            {
                var line = inputData[i].Replace("  ", " ").Trim();
                var row = line.Split(" ");
                State[i] = row;
            }
            CountUnmarked();
        }

        internal string[][] State { get; set; }
        internal int MarkedSum { get; set; } = 0;
        internal int UnmarkedSum { get; set; } = 0;
        

        internal bool MarkBoard(string number)
        {
            for (int i = 0; i < State.Length; i++)
            {
                for (int j = 0; j < State[i].Length; j++)
                {
                    if (State[i][j] == number)
                    {
                        State[i][j] = _marked;
                        MarkedSum += int.Parse(number);
                        UnmarkedSum -= int.Parse(number);
                        return true;
                    }
                }
            }
            return false;
        }

        private void CountUnmarked()
        {
            for (int i = 0; i < State.Length; i++)
            {
                for (int j = 0; j < State[i].Length; j++)
                {
                    var number = int.Parse(State[i][j]);
                    UnmarkedSum += number;
                }
            }
        }

        internal bool FindOnBoard(string numberCalled)
        {
            if (MarkBoard(numberCalled))
                return CheckVictory();
            else
                return false;
        }

        internal bool CheckVictory()
        {
            if (CheckRow())
                return true;
            if (CheckColumn())
                return true;
            return false;
        }

        private bool CheckRow()
        {
            for (int i = 0; i < State.Length; i++)
            {
                bool wholeRow = true;
                for (int j = 0; j < State[i].Length; j++)
                {
                    if (State[i][j] != _marked)
                    { 
                        wholeRow = false;
                        break;
                    }
                }
                if (wholeRow)
                    return true;
            }
            return false;
        }

        private bool CheckColumn()
        {
            for (int column = 0; column < State[0].Length; column++)
            {
                bool wholeColumn = true;
                for (int row = 0; row < State.Length; row++)
                {
                    if (State[column][row] != _marked)
                    {
                        wholeColumn = false;
                        break;
                    }
                }
                if (wholeColumn)
                    return true;
            }
            return false;
        }
    }
}

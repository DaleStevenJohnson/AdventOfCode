using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Schema;
using ChallengeSolutions.Abstractions;
using ChallengeSolutions.Helpers;

namespace ChallengeSolutions.Y2022
{
    public class Tree
    {
        public int MaxX;
        public int MaxY;
        public int X;
        public int Y;
        public int Height;
        public bool isVisible = false;
        public int ScenicScore;
        public Tree(int x, int y, int height, int maxX, int maxY)
        { 
            X = x;
            Y = y;
            MaxX= maxX;
            MaxY= maxY;
            Height = height;
            if (x == 0 || x == maxX || y == 0 || y == maxY)
                isVisible = true;
        }

        public bool CheckIsVisibleColumn(int start, int stop, List<string> data)
        { 
            bool currentlyVisible = true;
            for (int column = start; column < stop; column++)
            {
                var treeHeight = int.Parse(data[column][X].ToString());
                if (treeHeight >= Height)
                { 
                    currentlyVisible= false;
                    break;
                }
            }

            if (!isVisible)
                isVisible= currentlyVisible;

            return isVisible;
        }

        public bool CheckIsVisibleRow(int start, int stop, List<string> data)
        {
            bool currentlyVisible = true;
            for (int row = start; row < stop; row++)
            {
                var treeHeight = int.Parse(data[Y][row].ToString());
                if (treeHeight >= Height)
                {
                    currentlyVisible = false;
                    break;
                }
            }

            if (!isVisible)
                isVisible = currentlyVisible;

            return isVisible;
        }

        public bool CheckIsVisible(List<string> data)
        { 
            if (isVisible) return true;

            CheckIsVisibleColumn(0, Y, data);
            CheckIsVisibleColumn(Y+1, MaxY, data);
            CheckIsVisibleRow(0, X, data);
            CheckIsVisibleRow(X+1, MaxX, data);

            return isVisible;
        }

        public int CalculateScenicScore()
        {
            return ScenicScore;
        }
    }

      public class Y2022D8 : ISolution
    {
       
        public List<string> TestData { get; set; }
        public List<string> PuzzleData { get; set; }
        public Y2022D8()
        {
            TestData = MyIO.ReadStringsFromFile(2022, 8, 1, true);
            PuzzleData = MyIO.ReadStringsFromFile(2022, 8, 1, false);
        }

        public List<Tree> GenerateTrees(List<string> data)
        {
            var trees = new List<Tree>();
            for (int y = 0; y < data.Count; y++)
            {
                var row = data[y];
                for (int x = 0; x < row.Length; x++)
                {
                    var height = int.Parse(row[x].ToString());
                    var tree = new Tree(x, y, height, row.Length, data.Count);
                    trees.Add(tree);
                }
            }
            return trees;
        }
    
        public string SolvePart1(List<string> data)
        {
            var count = 0;
            var trees = GenerateTrees(data);
            foreach (var tree in trees)
            { 
                if (tree.CheckIsVisible(data)) { count++; }
            }
            return count.ToString();
        }

        

        public string SolvePart2(List<string> data)
        {
            return "";
        }
    }
}
 
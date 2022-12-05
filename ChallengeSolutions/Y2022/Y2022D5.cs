using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using ChallengeSolutions.Abstractions;
using ChallengeSolutions.Helpers;

namespace ChallengeSolutions.Y2022
{
  
    public class Y2022D5 : ISolution
    {
       
        public List<string> TestData { get; set; }
        public List<string> PuzzleData { get; set; }
        public Y2022D5()
        {
            TestData = MyIO.ReadStringsFromFile(2022, 5, 1, true);
            PuzzleData = MyIO.ReadStringsFromFile(2022, 5, 1, false);
        }

        private List<Stack<char>> CreateEmptyCrateStacks(int count)
        {
            var result = new List<Stack<char>>(); 
            for (int i = 0; i < count; i++)
            {
                var s = new Stack<char>();
                result.Add(s);
            }
            return result;
        }

        public List<Stack<char>> ReverseStacks(List<Stack<char>> oldStacks)
        { 
            var newStacks = CreateEmptyCrateStacks(oldStacks.Count);
            for (var currentStack = 0; currentStack < oldStacks.Count; currentStack++)
            {
                for (int i = oldStacks[currentStack].Count -1; i > -1 ; i--)
                {
                    newStacks[currentStack].Push(oldStacks[currentStack].Pop());
                }
            }
            return newStacks;
        }
        public List<Stack<char>> GenerateCrateStacks(List<string> data, out int index)
        {
            int size = (data[0].Length + 1) / 4;
            var crateStacks = CreateEmptyCrateStacks(size);
            index = 0;
            while (data[index].Contains("["))
            {
                var crate = 0;
                for (int i = 1; i < data[index].Length; i += 4)
                {
                    var item = data[index][i];
                    if(item != ' ')
                        crateStacks[crate].Push(item);
                    crate++;
                }
                index++;
            }
            return crateStacks;
        }

        private string PeekStacks(List<Stack<char>> stacks)
        {
            var result = string.Empty;
            foreach (var stack in stacks)
            {
                if (stack.Count > 0)
                {
                    var item = stack.Peek();
                    if (item != ' ')
                    {
                        result += item;
                    }
                }
                else
                {
                    result += "@";
                }
            }
            return result;
        }

        public List<Stack<char>> MoveCratesOneAtATime(List<string> data)
        {
            int index = 0;
            var crateStacks = GenerateCrateStacks(data, out index);
            crateStacks = ReverseStacks(crateStacks);
            index += 2;
            for (int i = index; i < data.Count; i++)
            {
                var instructions = data[i].Split(" ");
                var operations = int.Parse(instructions[1]);
                var origin = int.Parse(instructions[3]) - 1;
                var destination = int.Parse(instructions[5]) - 1;
                for (int j = 0; j < operations; j++)
                {
                    if (crateStacks[origin].Count > 0)
                    {
                        var item = crateStacks[origin].Pop();
                        crateStacks[destination].Push(item);
                    }
                }
            }
            return crateStacks;
        }

        public List<Stack<char>> MoveCratesTogether(List<string> data)
        {
            int index = 0;
            var crateStacks = GenerateCrateStacks(data, out index);
            crateStacks = ReverseStacks(crateStacks);
            index += 2;
            for (int i = index; i < data.Count; i++)
            {
                var instructions = data[i].Split(" ");
                var operations = int.Parse(instructions[1]);
                var origin = int.Parse(instructions[3]) - 1;
                var destination = int.Parse(instructions[5]) - 1;
                var s = new Stack<char>();
                for (int j = 0; j < operations; j++)
                {
                    if (crateStacks[origin].Count > 0)
                    {
                        var item = crateStacks[origin].Pop();
                        s.Push(item);
                    }
                }
                foreach (var item in s)
                {
                    crateStacks[destination].Push(item);
                }
                
            }
            return crateStacks;
        }

        public string PrintStacks(List<Stack<char>> stacks)
        {
            
            var result = string.Empty;
            var count = 1;
            foreach (var stack in stacks)
            {
                
                result += $"{count} --> ";
                foreach (var s in stack)
                {
                    result += $"{s} ";
                }
                result += "\n";
                count++;
            }
            return result;
        }

        public string SolvePart1(List<string> data)
        {
            var crateStacks = MoveCratesOneAtATime(data);
            var result = PeekStacks(crateStacks);
            return result;
        }

        

        public string SolvePart2(List<string> data)
        {
            var crateStacks = MoveCratesTogether(data);
            var result = PeekStacks(crateStacks);
            return result;
        }
    }
}
 
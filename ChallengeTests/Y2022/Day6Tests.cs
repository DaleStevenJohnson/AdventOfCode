using System.Collections.Generic;
using ChallengeSolutions.Abstractions;
using ChallengeSolutions.Y2022;
using NUnit.Framework;
using static System.Formats.Asn1.AsnWriter;

namespace ChallengeTests.Y2022
{
    internal class Day6Tests
    {
        [Test]
        public void Part1SampleData()
        {
            ISolution day = new Y2022D6();
            var result = day.SolvePart1(day.TestData);
            Assert.That(result, Is.EqualTo("6"));
        }
        [Test]
        public void Part1PuzzleData()
        {
            ISolution day = new Y2022D6();
            var result = day.SolvePart1(day.PuzzleData);
            Assert.That(result, Is.EqualTo("1198"));
        }
        [Test]
        public void Part2SampleData()
        {
            ISolution day = new Y2022D6();
            var result = day.SolvePart2(day.TestData);
            Assert.That(result, Is.EqualTo("19"));
        }
        [Test]
        public void Part2PuzzleData()
        {
            ISolution day = new Y2022D6();
            var result = day.SolvePart2(day.PuzzleData);
            Assert.That(result, Is.EqualTo("?"));
        }

        [Test]
        public void Part1Debug()
        {
            var day = new Y2022D6();
            var dataStream = day.TestData[0];
            Queue<string> queue = new Queue<string>();
            var count = 0;
            foreach (var letter in dataStream)
            {
                if (queue.Count == 4)
                {
                    if (day.QueueHasUniqueValues(queue))
                        break;
                    else
                        queue.Dequeue();
                }
                queue.Enqueue(letter.ToString());
                count++;

                TestContext.WriteLine($"Count: {count} || Queue: {day.PrintQueue(queue)}");
            }
            
            
            Assert.That(count.ToString(), Is.EqualTo("6"));
        }

    }
}

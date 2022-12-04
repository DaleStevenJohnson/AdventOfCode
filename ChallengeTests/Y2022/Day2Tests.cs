using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeSolutions.Abstractions;
using ChallengeSolutions.Y2022;
using NUnit.Framework;

namespace ChallengeTests.Y2022
{
    internal class Day2Tests
    {
        [Test]
        public void Part1SampleData()
        {
            ISolution day = new Y2022D2();
            var result = day.SolvePart1(day.TestData);
            Assert.That(result, Is.EqualTo("15"));
        }
        [Test]
        public void Part1PuzzleData()
        {
            ISolution day = new Y2022D2();
            var result = day.SolvePart1(day.PuzzleData);
            Assert.That(result, Is.EqualTo("11150"));
        }
        [Test]
        public void Part2SampleData()
        {
            ISolution day = new Y2022D2();
            var result = day.SolvePart2(day.TestData);
            Assert.That(result, Is.EqualTo("12"));
        }
        [Test]
        public void Part2PuzzleData()
        {
            ISolution day = new Y2022D2();
            var result = day.SolvePart2(day.PuzzleData);
            Assert.That(result, Is.EqualTo("8295"));
        }
    }
}

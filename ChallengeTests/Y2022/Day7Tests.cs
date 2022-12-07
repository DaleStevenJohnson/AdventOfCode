using System.Collections.Generic;
using ChallengeSolutions.Abstractions;
using ChallengeSolutions.Y2022;
using NUnit.Framework;
using static System.Formats.Asn1.AsnWriter;

namespace ChallengeTests.Y2022
{
    internal class Day7Tests
    {
        [Test]
        public void Part1SampleData()
        {
            ISolution day = new Y2022D7();
            var result = day.SolvePart1(day.TestData);
            Assert.That(result, Is.EqualTo("95437"));
        }
        [Test]
        public void Part1PuzzleData()
        {
            ISolution day = new Y2022D7();
            var result = day.SolvePart1(day.PuzzleData);
            Assert.That(result, Is.EqualTo("?"));
        }
        [Test]
        public void Part2SampleData()
        {
            ISolution day = new Y2022D7();
            var result = day.SolvePart2(day.TestData);
            Assert.That(result, Is.EqualTo("?"));
        }
        [Test]
        public void Part2PuzzleData()
        {
            ISolution day = new Y2022D7();
            var result = day.SolvePart2(day.PuzzleData);
            Assert.That(result, Is.EqualTo("?"));
        }


    }
}

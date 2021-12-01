using ChallengeSolutions.Y2021;
using ChallengeSolutions.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace ChallengeTests.Y2021
{
    public class Day1Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ReadDay1TestDataStringsTest()
        {
            var stringPuzzleInput = MyIO.ReadStringsFromFile(2021, 1, 1, true);
            var stringTestData = new List<string>() { "199", "200", "208", "210", "200", "207", "240", "269", "260", "263"};
            Assert.That(stringPuzzleInput, Is.EquivalentTo(stringTestData));
        }

        [Test]
        public void ReadDay1TestDataIntsTest()
        {
            var intPuzzleInput = MyIO.ReadIntsFromFile(2021, 1, 1, true);
            var intTestData = new List<int>() { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
            Assert.That(intPuzzleInput, Is.EquivalentTo(intTestData));
        }

        [Test]
        public void SolveDay1WithTestData()
        {
            var result = Y2021D1.SolvePart1(true);
            Assert.AreEqual("7", result);
        }

        [Test]
        public void SolveDay1WithPuzzleData()
        {
            var result = Y2021D1.SolvePart1(false);
            Assert.AreEqual("1448", result);
        }

        [Test]
        public void SolveDay2WithTestData()
        {
            var result = Y2021D1.SolvePart2(true);
            Assert.AreEqual("5", result);
        }

        [Test]
        public void SolveDay2WithPuzzleData()
        {
            var result = Y2021D1.SolvePart2(false);
            Assert.AreEqual("1471", result);
        }
    }
}
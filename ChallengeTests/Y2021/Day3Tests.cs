using ChallengeSolutions.Y2021;
using ChallengeSolutions.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace ChallengeTests.Y2021
{
    public class Day3Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ReadDay3TestDataStringsTest()
        {
            var stringPuzzleInput = MyIO.ReadStringsFromFile(2021, 3, 1, true);
            var stringTestData = new List<string>() { "00100","11110", "10110","10111","10101","01111","00111","11100","10000","11001","00010","01010"};
            Assert.That(stringPuzzleInput, Is.EquivalentTo(stringTestData));
        }

        [Test]
        public void SolvePart1WithTestData()
        {
            var result = Y2021D3.SolvePart1(true);
            Assert.AreEqual("198", result);
        }

        [Test]
        public void SolvePart1WithPuzzleData()
        {
            var result = Y2021D3.SolvePart1(false);
            Assert.AreEqual("2954600", result);
        }

        [Test]
        public void SolvePart2WithTestData()
        {
            var result = Y2021D3.SolvePart2(true);
            Assert.AreEqual("230", result);
        }

        [Test]
        public void SolvePart2WithPuzzleData()
        {
            var result = Y2021D3.SolvePart2(false);
            Assert.AreEqual("1662846", result);
        }
    }
}
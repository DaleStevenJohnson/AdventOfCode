using ChallengeSolutions.Y2021;
using ChallengeSolutions.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace ChallengeTests.Y2021
{
    public class Day2Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ReadDay2TestDataStringsTest()
        {
            var stringPuzzleInput = MyIO.ReadStringsFromFile(2021, 2, 1, true);
            var stringTestData = new List<string>() { "forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2" };
            Assert.That(stringPuzzleInput, Is.EquivalentTo(stringTestData));
        }


        [Test]
        public void SolvePart1WithTestData()
        {
            var result = Y2021D2.SolvePart1(true);
            Assert.AreEqual("150", result);
        }

        [Test]
        public void SolvePart1WithPuzzleData()
        {
            var result = Y2021D2.SolvePart1(false);
            Assert.AreEqual("1693300", result);
        }

        [Test]
        public void SolvePart2WithTestData()
        {
            var result = Y2021D2.SolvePart2(true);
            Assert.AreEqual("900", result);
        }

        [Test]
        public void SolvePart2WithPuzzleData()
        {
            var result = Y2021D2.SolvePart2(false);
            Assert.AreEqual("1857958050", result);
        }
    }
}
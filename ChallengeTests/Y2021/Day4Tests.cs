using ChallengeSolutions.Y2021;
using ChallengeSolutions.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace ChallengeTests.Y2021
{
    public class Day4Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ReadDay4TestDataStringsTest()
        {
            var stringPuzzleInput = MyIO.ReadStringsFromFile(2021, 4, 1, true);
            var stringTestData = new List<string>()
            {
                "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1",
                ""              ,
                "22 13 17 11  0",
                " 8  2 23  4 24",
                "21  9 14 16  7",
                " 6 10  3 18  5",
                " 1 12 20 15 19",
                ""              ,
                " 3 15  0  2 22",
                " 9 18 13 17  5",
                "19  8  7 25 23",
                "20 11 10 24  4",
                "14 21 16 12  6",
                ""              ,
                "14 21 17 24  4",
                "10 16 15  9 19",
                "18  8 23 26 20",
                "22 11 13  6  5",
                " 2  0 12  3  7"
            };
            Assert.That(stringPuzzleInput, Is.EquivalentTo(stringTestData));
        }

        [Test]
        public void SolvePart1WithTestData()
        {
            var result = Y2021D4.SolvePart1(true);
            Assert.AreEqual("4512", result);
        }

        [Test]
        public void SolvePart1WithPuzzleData()
        {
            var result = Y2021D4.SolvePart1(false);
            Assert.AreEqual("2954600", result);
        }

        [Test]
        public void SolvePart2WithTestData()
        {
            var result = Y2021D4.SolvePart2(true);
            Assert.AreEqual("230", result);
        }

        [Test]
        public void SolvePart2WithPuzzleData()
        {
            var result = Y2021D4.SolvePart2(false);
            Assert.AreEqual("1662846", result);
        }
    }
}
using ChallengeSolutions.Abstractions;
using ChallengeSolutions.Y2022;
using NUnit.Framework;
using static System.Formats.Asn1.AsnWriter;

namespace ChallengeTests.Y2022
{
    internal class Day5Tests
    {
        [Test]
        public void Part1SampleData()
        {
            ISolution day = new Y2022D5();
            var result = day.SolvePart1(day.TestData);
            Assert.That(result, Is.EqualTo("CMZ"));
        }
        [Test]
        public void Part1PuzzleData()
        {
            ISolution day = new Y2022D5();
            var result = day.SolvePart1(day.PuzzleData);
            Assert.That(result, Is.EqualTo("VWLCWGSDQ"));
        }
        [Test]
        public void Part2SampleData()
        {
            ISolution day = new Y2022D5();
            var result = day.SolvePart2(day.TestData);
            Assert.That(result, Is.EqualTo("MCD"));
        }
        [Test]
        public void Part2PuzzleData()
        {
            ISolution day = new Y2022D5();
            var result = day.SolvePart2(day.PuzzleData);
            Assert.That(result, Is.EqualTo("TCGLQSLPW"));
        }
        //[Test]
        //public void PrintStacks()
        //{
        //    Y2022D5 day = new Y2022D5();
        //    var crates = day.MoveCratesOneAtATime(day.TestData);
        //    var result = day.PrintStacks(crates);
        //    TestContext.WriteLine(result);
        //    Assert.That(result, Is.EqualTo("CMZ"));
        //}

        //[Test]
        //public void CheckStartingStacks()
        //{
        //    Y2022D5 day = new Y2022D5();
        //    var crates = day.GenerateCrateStacks(day.TestData, out int _);
        //    var result = day.PrintStacks(crates);
        //    TestContext.WriteLine(result);
        //    Assert.That(result, Is.EqualTo("CMZ"));
        //}


        //[Test]
        //public void ChecReversingStacks()
        //{
        //    Y2022D5 day = new Y2022D5();
        //    var crates = day.GenerateCrateStacks(day.TestData, out int _);
        //    var result = day.PrintStacks(day.ReverseStacks(crates));
        //    TestContext.WriteLine(result);
        //    Assert.That(result, Is.EqualTo("CMZ"));
        //}
    }
}

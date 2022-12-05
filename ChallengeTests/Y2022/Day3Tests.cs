using ChallengeSolutions.Abstractions;
using ChallengeSolutions.Y2022;
using NUnit.Framework;
using static System.Formats.Asn1.AsnWriter;

namespace ChallengeTests.Y2022
{
    internal class Day3Tests
    {
        [Test]
        public void Part1SampleData()
        {
            ISolution day = new Y2022D3();
            var result = day.SolvePart1(day.TestData);
            Assert.That(result, Is.EqualTo("157"));
        }
        [Test]
        public void Part1PuzzleData()
        {
            ISolution day = new Y2022D3();
            var result = day.SolvePart1(day.PuzzleData);
            Assert.That(result, Is.EqualTo("7908"));
        }
        [Test]
        public void Part2SampleData()
        {
            ISolution day = new Y2022D3();
            var result = day.SolvePart2(day.TestData);
            Assert.That(result, Is.EqualTo("70"));
        }
        [Test]
        public void Part2PuzzleData()
        {
            ISolution day = new Y2022D3();
            var result = day.SolvePart2(day.PuzzleData);
            Assert.That(result, Is.EqualTo("2838"));
        }
        [Test, Sequential]
        public void TestPriorityScoreLowerCase([Range(97, 122)] int ascii, [Range(1, 26)] int score)
        {
            var day = new Y2022D3();
            var input = ((char)ascii).ToString();
            var result = day.GetPriorityScore(input);
            Assert.That(result, Is.EqualTo(score));
        }
        [Test, Sequential]
        public void TestPriorityScoreUpperCase([Range(65, 90)] int ascii, [Range(27, 52)] int score)
        {
            var day = new Y2022D3();
            var input = ((char)ascii).ToString();
            var result = day.GetPriorityScore(input);
            Assert.That(result, Is.EqualTo(score));
        }
        [Test, Sequential]
        public void TestBackpackSearch([Values("AbcciBAt", "BBB", "AaghyYytszzzzzzast")] string backpack, [Values("A", "B", "ats")] string expected)
        {
            var day = new Y2022D3();
            var result = day.SearchBackpackForDuplicates(backpack);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}

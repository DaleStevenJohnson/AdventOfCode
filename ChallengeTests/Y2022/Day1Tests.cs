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
    internal class Day1Tests
    {
        [Test]
        public void Day1SampleData()
        {
            ISolution day = new Y2022D1();
            var result = day.SolveDay1(day.TestData);
            Assert.That(result, Is.EqualTo("24000"));
        }
        [Test]
        public void Day1PuzzleData()
        {
            ISolution day = new Y2022D1();
            var result = day.SolveDay1(day.PuzzleData);
            Assert.That(result, Is.EqualTo("72718"));
        }
        [Test]
        public void Day2SampleData()
        {
            ISolution day = new Y2022D1();
            var result = day.SolveDay2(day.TestData);
            Assert.That(result, Is.EqualTo("45000"));
        }
        [Test]
        public void Day2PuzzleData()
        {
            ISolution day = new Y2022D1();
            var result = day.SolveDay2(day.PuzzleData);
            Assert.That(result, Is.EqualTo("213089"));
        }
    }
}

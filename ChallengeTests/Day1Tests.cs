using ChallengeSolutions.Y2021;
using NUnit.Framework;

namespace ChallengeTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var result = Y2021D1.SolveDay1(false);
            Assert.Pass();
        }
    }
}
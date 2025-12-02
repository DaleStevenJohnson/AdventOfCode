using ChallengeSolutions.Helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace ChallengeTests.Helpers
{
    public class BinaryFunctionTests
    {
        [SetUp]
        public void Setup()
        { 
        
        }

        [Test]
        public void ConvertBinaryStringsToInt()
        {
            var binary = "00001010";
            var result = BinaryFunctions.ConvertStringToInt(binary);
            Assert.AreEqual(10, result);
        }
    }
}

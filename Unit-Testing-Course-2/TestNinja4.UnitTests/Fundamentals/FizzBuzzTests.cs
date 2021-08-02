using NUnit.Framework;
using System;
using TestNinja4.Fundamentals;

namespace TestNinja4.UnitTests
{
    [TestFixture]
    class FizzBuzzTests
    {
        [Test]
        public void GetOuput_InputDivisibleBy3And5_ReturnsFizzBuzz()
        {
            string result = FizzBuzz.GetOutput(15);

            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }
        [Test]
        public void GetOuput_InputDivisibleBy3_ReturnsFizz()
        {
            string result = FizzBuzz.GetOutput(3);

            Assert.That(result, Is.EqualTo("Fizz"));
        }
        [Test]
        public void GetOuput_InputDivisibleBy5_ReturnsBuzz()
        {
            string result = FizzBuzz.GetOutput(5);

            Assert.That(result, Is.EqualTo("Buzz"));
        }
        [Test]
        public void GetOuput_InputNotDivisibleBy3Or5_ReturnsSameNumber()
        {
            string result = FizzBuzz.GetOutput(2);

            Assert.That(result, Is.EqualTo("2"));
        }
    }
}

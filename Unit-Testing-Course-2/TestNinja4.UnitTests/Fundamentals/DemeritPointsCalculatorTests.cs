using NUnit.Framework;
using System;
using TestNinja4.Fundamentals;

namespace TestNinja4.UnitTests
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _demeritPointsCalculator;
        [SetUp]
        public void SetUp()
        {
            _demeritPointsCalculator = new();   
        }
        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        public void CalculateDemeritPoints_SpeedOutOfRange_ThrowArgumentOutOfRangeException(int speed)
        {
            Assert.That(() => _demeritPointsCalculator.CalculateDemeritPoints(speed),
                Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }
        [Test]
        [TestCase(0, 0)]
        [TestCase(64, 0)]
        [TestCase(65, 0)]
        [TestCase(66, 0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        public void CalculateDemeritPoints_WhenCalled_ReturnDemeritsPoints(int speed, int expected)
        {
            Assert.That(_demeritPointsCalculator.CalculateDemeritPoints(speed),
                Is.EqualTo(expected));
        }
    }
}

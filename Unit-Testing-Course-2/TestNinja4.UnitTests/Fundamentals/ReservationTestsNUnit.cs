using NUnit.Framework;
using TestNinja4.Fundamentals;

namespace TestNinja4.UnitTests
{    
    [TestFixture]
    public class ReservationTestsNUnit
    {
        [Test]
        public void CanBeCancelledBy_AdmingCancelling_ReturnsTrue()
        {
            //Arrange
            var reservation = new Reservation();

            //Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            //Assert
            Assert.That(result, Is.True);

        }
        [Test]
        public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
        {
            //Arrange            
            var user = new User { IsAdmin = false };
            var reservation = new Reservation { MadeBy = user };

            //Act
            var result = reservation.CanBeCancelledBy(user);

            //Assert
            Assert.That(result, Is.True);

        }
        [Test]
        public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
        {
            //Arrange

            var reservation = new Reservation { MadeBy = new User() };

            //Act
            var result = reservation.CanBeCancelledBy(new User());

            //Assert
            Assert.That(result, Is.False);

        }

    }
}

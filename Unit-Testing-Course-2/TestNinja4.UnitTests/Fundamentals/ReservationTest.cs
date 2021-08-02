using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestNinja4.Fundamentals;

namespace TestNinja4.UnitTests
{
    [TestClass]
    public class ReservationTests
    {
        [TestMethod]
        public void CanBeCancelledBy_AdmingCancelling_ReturnsTrue()
        {
            //Arrange
            var reservation = new Reservation();

            //Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            //Assert
            Assert.IsTrue(result);

        }
        [TestMethod]
        public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
        {
            //Arrange            
            var user = new User { IsAdmin = false };
            var reservation = new Reservation { MadeBy = user };

            //Act
            var result = reservation.CanBeCancelledBy(user);

            //Assert
            Assert.IsTrue(result);

        }
        [TestMethod]
        public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
        {
            //Arrange
            //var user = new User { isAdmin = false };
            //var user2 = new User { isAdmin = false };
            //var reservation = new Reservation { MadeBy = user };
            var reservation = new Reservation { MadeBy = new User() };

            //Act
            var result = reservation.CanBeCancelledBy(new User());

            //Assert
            Assert.IsFalse(result);

        }

    }
}

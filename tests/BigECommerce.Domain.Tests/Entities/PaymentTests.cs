using BigECommerce.Domain.Entities;
using BigECommerce.Domain.Enums;

namespace BigECommerce.Domain.Tests.Entities
{
    public class PaymentTests
    {
        [Fact]
        public void CreatePayment_ShouldInitializeWithPendingStatus()
        {
            // Arrange & Act
            var payment = new Payment(Guid.NewGuid(), PaymentMethod.Pix);

            // Assert
            Assert.Equal(PaymentStatus.Pending, payment.Status);
            Assert.NotEqual(Guid.Empty, payment.Id);
        }

        [Fact]
        public void Confirm_ShouldChangeStatusToConfirmed()
        {
            // Arrange
            var payment = new Payment(Guid.NewGuid(), PaymentMethod.Pix);

            // Act
            payment.Confirm();

            // Assert
            Assert.Equal(PaymentStatus.Confirmed, payment.Status);
        }
    }
}

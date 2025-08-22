using BigECommerce.Domain.Entities;
using BigECommerce.Domain.Enums;

namespace BigECommerce.Domain.Tests.Entities
{
    public class OrderTests
    {
        [Fact]
        public void CreateOrder_ShouldInitializeWithPendingStatus()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var order = new Order(userId);
            order.AddItem(new OrderItem(order.Id, Guid.NewGuid(), 2, 10));

            // Assert
            Assert.Equal(userId, order.UserId);
            Assert.Equal(OrderStatus.Pending, order.Status);
            Assert.NotEqual(Guid.Empty, order.Id);
            Assert.NotEmpty(order.Items);
        }

        [Fact]
        public void AddItem_ShouldAddItemToList()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());
            var item = new OrderItem(order.Id, Guid.NewGuid(), 2, 10);

            // Act
            order.AddItem(item);

            // Assert
            Assert.Contains(item, order.Items);
            Assert.Single(order.Items);
        }

        [Fact]
        public void LinkPayment_ShouldAssociatePaymentToOrder()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());
            var payment = new Payment(order.Id, PaymentMethod.Card);

            // Act
            order.LinkPayment(payment);

            // Assert
            Assert.Equal(payment.Id, order.PaymentId);
            Assert.Equal(payment, order.Payment);
        }

        [Fact]
        public void LinkPayment_ShouldThrowException_WhenPaymentIsNull()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => order.LinkPayment(null!));
        }
        [Fact]
        public void ConfirmPayment_ShouldChangeStatusToPaid_WhenOrderIsPending()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());

            // Act
            order.ConfirmPayment();

            // Assert
            Assert.Equal(OrderStatus.Paid, order.Status);
        }

        [Fact]
        public void ConfirmPayment_ShouldThrow_WhenOrderIsNotPending()
        {
            // Arrange
            var order = new Order(Guid.NewGuid());
            order.ConfirmPayment(); // Alterando status do pedido para pago.

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => order.ConfirmPayment()); // Forçando exceção.
        }
    }
}

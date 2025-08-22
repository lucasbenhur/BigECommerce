using BigECommerce.Domain.Enums;

namespace BigECommerce.Domain.Entities
{
    public class Order
    {
        protected Order()
        {
        }

        public Order(
            Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CreatedAt = DateTimeOffset.Now;
            Status = OrderStatus.Pending;
        }

        public Guid Id { get; internal set; }
        public Guid UserId { get; internal set; }
        public DateTimeOffset CreatedAt { get; internal set; }
        public OrderStatus Status { get; internal set; }
        public List<OrderItem> Items { get; internal set; } = new List<OrderItem>();
        public Guid PaymentId { get; internal set; }
        public Payment Payment { get; internal set; } = null!;

        public void AddItem(OrderItem item) => Items.Add(item);

        public void LinkPayment(Payment payment)
        {
            if (payment is null)
                throw new ArgumentNullException(nameof(payment));

            PaymentId = payment.Id;
            Payment = payment;
        }

        public void ConfirmPayment()
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Pedido não está pendente.");

            Status = OrderStatus.Paid;
        }
    }
}

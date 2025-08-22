using BigECommerce.Domain.Enums;

namespace BigECommerce.Domain.Entities
{
    public class Payment
    {
        protected Payment() { }

        public Guid Id { get; internal set; }
        public Guid OrderId { get; internal set; }
        public Order Order { get; internal set; } = null!;
        public PaymentMethod Method { get; internal set; }
        public PaymentStatus Status { get; internal set; }

        public Payment(Guid orderId, PaymentMethod method)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            Method = method;
            Status = PaymentStatus.Pending;
        }

        public void Confirm() => Status = PaymentStatus.Confirmed;
    }
}

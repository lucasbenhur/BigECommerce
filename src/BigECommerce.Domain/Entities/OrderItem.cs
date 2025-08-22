namespace BigECommerce.Domain.Entities
{
    public class OrderItem
    {
        protected OrderItem()
        {
        }

        public OrderItem(
            Guid orderId,
            Guid productId,
            int quantity,
            decimal unitPrice)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public Guid Id { get; internal set; }
        public Guid OrderId { get; internal set; }
        public Guid ProductId { get; internal set; }
        public Product Product { get; internal set; } = null!;
        public int Quantity { get; internal set; }
        public decimal UnitPrice { get; internal set; }
    }
}

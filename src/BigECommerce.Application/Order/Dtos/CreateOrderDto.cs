namespace BigECommerce.Application.Order.Dtos
{
    public class CreateOrderDto
    {
        public string PaymentMethod { get; set; }
        public List<CreateOrderItemDto> Items { get; set; } = new List<CreateOrderItemDto>();
    }
}

using BigECommerce.Application.Order.Dtos;
using BigECommerce.Domain.Enums;
using MediatR;

namespace BigECommerce.Application.Order.Commands
{
    public record CreateOrderCommand(PaymentMethod PaymentMethod, List<CreateOrderItemDto> Items) : IRequest<Guid>;
}

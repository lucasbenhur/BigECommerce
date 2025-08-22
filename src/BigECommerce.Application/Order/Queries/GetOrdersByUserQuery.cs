using BigECommerce.Application.Order.Dtos;
using MediatR;

namespace BigECommerce.Application.Order.Queries
{
    public class GetOrdersByUserQuery : IRequest<List<OrderDto>>
    {
    }
}

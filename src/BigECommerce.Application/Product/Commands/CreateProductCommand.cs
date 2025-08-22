using MediatR;

namespace BigECommerce.Application.Product.Commands
{
    public record CreateProductCommand(string Name, decimal Price, int Stock) : IRequest<Guid>;
}

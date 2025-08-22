using MediatR;

namespace BigECommerce.Application.Product.Commands
{
    public record UpdateProductCommand(Guid Id, string Name, decimal Price, int Stock) : IRequest<bool>;
}

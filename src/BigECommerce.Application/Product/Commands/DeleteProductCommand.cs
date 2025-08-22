using MediatR;

namespace BigECommerce.Application.Product.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest<bool>;
}

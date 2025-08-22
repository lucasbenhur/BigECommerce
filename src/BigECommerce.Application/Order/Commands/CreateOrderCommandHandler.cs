using BigECommerce.Domain.Entities;
using BigECommerce.Infrastructure.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BigECommerce.Application.Order.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly BigECommerceDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateOrderCommandHandler(
            BigECommerceDbContext dbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
                throw new UnauthorizedAccessException("Não foi possível obter o id do usuário autenticado.");

            var order = new Domain.Entities.Order(userId);

            foreach (var item in request.Items)
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId, cancellationToken);

                if (product is null)
                {
                    throw new InvalidOperationException($"Produto {item.ProductId} não encontrado.");
                }
                else if (product.Stock < item.Quantity)
                {
                    throw new InvalidOperationException($"Produto {product.Name} sem estoque.");
                }

                product.SubtractStock(item.Quantity);
                _dbContext.Products.Update(product);

                var orderItem = new OrderItem(order.Id, item.ProductId, item.Quantity, product.Price);
                order.AddItem(orderItem);
            }

            var payment = new Domain.Entities.Payment(order.Id, request.PaymentMethod);
            order.LinkPayment(payment);

            _dbContext.Orders.Add(order);
            _dbContext.Payments.Add(payment);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}

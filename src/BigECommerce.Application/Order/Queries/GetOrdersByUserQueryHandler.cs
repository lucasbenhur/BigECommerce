using BigECommerce.Application.Order.Dtos;
using BigECommerce.Infrastructure.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BigECommerce.Application.Order.Queries
{
    public class GetOrdersByUserQueryHandler : IRequestHandler<GetOrdersByUserQuery, List<OrderDto>>
    {
        private readonly BigECommerceDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetOrdersByUserQueryHandler(
            BigECommerceDbContext dbContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<OrderDto>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
                throw new UnauthorizedAccessException("Não foi possível obter o id do usuário autenticado.");

            var orders = await _dbContext.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                CreatedAt = o.CreatedAt,
                Status = o.Status.ToString(),
                TotalAmount = o.Items.Sum(i => i.UnitPrice * i.Quantity),
                Items = o.Items.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity
                }).ToList(),
                PaymentId = o.PaymentId
            }).ToList();
        }
    }
}

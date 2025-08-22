using BigECommerce.Application.Product.Dtos;
using BigECommerce.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BigECommerce.Application.Product.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly BigECommerceDbContext _dbContext;

        public GetAllProductsQueryHandler(BigECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .Where(x => string.IsNullOrWhiteSpace(request.Search) || x.Name.ToLower().Contains(request.Search.ToLower()))
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock
                })
                .ToListAsync(cancellationToken);
        }
    }
}

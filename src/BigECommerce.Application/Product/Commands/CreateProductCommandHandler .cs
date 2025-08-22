using BigECommerce.Infrastructure.DbContexts;
using MediatR;

namespace BigECommerce.Application.Product.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly BigECommerceDbContext _dbContext;

        public CreateProductCommandHandler(BigECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Domain.Entities.Product(request.Name, request.Price, request.Stock);
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product.Id;
        }
    }
}

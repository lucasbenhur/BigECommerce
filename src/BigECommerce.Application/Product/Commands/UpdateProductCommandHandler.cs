using BigECommerce.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BigECommerce.Application.Product.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly BigECommerceDbContext _dbContext;

        public UpdateProductCommandHandler(BigECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
                return false;

            product.Update(request.Name, request.Price, request.Stock);
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

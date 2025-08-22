using BigECommerce.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BigECommerce.Application.Product.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly BigECommerceDbContext _dbContext;

        public DeleteProductCommandHandler(BigECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
                return false;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

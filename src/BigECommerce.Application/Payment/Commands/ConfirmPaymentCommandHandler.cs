using BigECommerce.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BigECommerce.Application.Payment.Commands
{
    public class ConfirmPaymentCommandHandler : IRequestHandler<ConfirmPaymentCommand, bool>
    {
        private readonly BigECommerceDbContext _dbContext;

        public ConfirmPaymentCommandHandler(BigECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _dbContext.Payments
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.Id == request.id, cancellationToken);

            if (payment == null)
                return false;

            payment.Confirm();
            payment.Order.ConfirmPayment();
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

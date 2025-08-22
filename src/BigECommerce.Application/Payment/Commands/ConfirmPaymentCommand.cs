using MediatR;

namespace BigECommerce.Application.Payment.Commands
{
    public record ConfirmPaymentCommand(Guid id) : IRequest<bool>;
}

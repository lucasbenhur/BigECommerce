using BigECommerce.Application.Payment.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigECommerce.Api.Controllers.Application
{
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Confirma o pagamento e atualiza o pedido para pago.
        /// </summary>
        [HttpPost("{id}/confirm")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> ConfirmPayment([FromRoute] Guid id)
        {
            try
            {
                var success = await _mediator.Send(new ConfirmPaymentCommand(id));

                if (!success)
                    return BadRequest("Pagamento não confirmado.");

                return Ok("Pagamento confirmado e pedido atualizado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao confirmar o pagamento: {ex.Message}");
            }
        }
    }
}

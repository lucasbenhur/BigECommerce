using BigECommerce.Application.Order.Commands;
using BigECommerce.Application.Order.Dtos;
using BigECommerce.Application.Order.Queries;
using BigECommerce.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigECommerce.Api.Controllers.Application
{
    [Route("api/orders")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um pedido para o cliente logado. Os Métodos de pagamento aceitos são Pix e Cartão.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            try
            {
                var paymentMethod = dto.PaymentMethod.ToLower() switch
                {
                    "pix" => PaymentMethod.Pix,
                    "card" => PaymentMethod.Card,
                    "cartão" => PaymentMethod.Card,
                    "cartao" => PaymentMethod.Card,
                    _ => throw new InvalidOperationException("Método de pagamento inválido. Os valores permtidos são \"Pix\" e \"Cartão\".")
                };

                var orderId = await _mediator.Send(new CreateOrderCommand(paymentMethod, dto.Items));
                return Ok(orderId);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar pedido: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna os pedidos do cliente logado.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetOrdersByUser()
        {
            try
            {
                var orders = await _mediator.Send(new GetOrdersByUserQuery());

                if (orders.Count == 0)
                    return NoContent();

                return Ok(orders);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao consultar os pedidos. Detalhes: {ex.Message}.");
            }
        }
    }
}

using BigECommerce.Application.Product.Commands;
using BigECommerce.Application.Product.Dtos;
using BigECommerce.Application.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigECommerce.Api.Controllers.Application
{
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cadastra um produto.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao criar o produto. Detalhes: {ex.Message}.");
            }
        }

        /// <summary>
        /// Lista os produtos cadastrados.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> GetAll([FromQuery] string search)
        {
            try
            {
                var products = await _mediator.Send(new GetAllProductsQuery(search));

                if (products?.Count == 0)
                    return NoContent();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao listar os produtos. Detalhes: {ex.Message}.");
            }
        }

        /// <summary>
        /// Atualiza o produto com id especificado.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductDto dto)
        {
            try
            {
                var success = await _mediator.Send(new UpdateProductCommand(id, dto.Name, dto.Price, dto.Stock));

                if (!success)
                    return BadRequest("Não foi possível atualizar o produto verifique os dados informados!");

                return Ok("Produto atualizado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao atualizar o produto. Detalhes: {ex.Message}.");
            }
        }

        /// <summary>
        /// Exclui o produto com id especificado.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var success = await _mediator.Send(new DeleteProductCommand(id));

                if (!success)
                    return BadRequest("Não foi possível excluir o produto verifique!");

                return Ok("Produto excluido.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro excluir o produto. Detalhes: {ex.Message}.");
            }
        }
    }
}

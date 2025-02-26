using Microsoft.AspNetCore.Mvc;
using Pedido10.Domain.Dto;
using Pedido10.Application.Service;
using Pedido10.Domain.Entity;
using Pedido10.Application.Contract;

namespace Pedido10.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _pedidoService;

        public PedidoController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidos = await _pedidoService.GetAll();
            if (pedidos == null)
            {
                return NotFound(new { resultado = "Nenhum pedido Cadastrado" });
            }

            return Ok(new { sucesses = true, message = "Pedidos encontrados", result = pedidos });
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] PedidoCreateDto pedidoDTO)
        {
            if (pedidoDTO == null)
                return BadRequest("Pedido inválido.");

            Pedido pedidoCriado = await _pedidoService.CriarPedidoAsync(pedidoDTO);
            return CreatedAtAction(nameof(CriarPedido), new { id = pedidoCriado.ID_Pedido }, pedidoCriado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _pedidoService.Delete(id);
            if (!sucesso)
                return NotFound("Pedido não encontrado.");

            return NoContent();
        }
    }
}

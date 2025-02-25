using Microsoft.AspNetCore.Mvc;
using Pedido10.Domain.Dto;
using Pedido10.Application.Service;
using Pedido10.Domain.Entity;

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

        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] PedidoCreateDto pedidoDTO)
        {
            if (pedidoDTO == null)
                return BadRequest("Pedido inválido.");

            Pedido pedidoCriado = await _pedidoService.CriarPedidoAsync(pedidoDTO);
            return CreatedAtAction(nameof(CriarPedido), new { id = pedidoCriado.ID_Pedido }, pedidoCriado);
        }
    }
}

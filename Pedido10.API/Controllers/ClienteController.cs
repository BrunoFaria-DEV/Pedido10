using Microsoft.AspNetCore.Mvc;
using Pedido10.Application.Contract;
using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;
using System.Text.RegularExpressions;

namespace Pedido10.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    { 

        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _clienteService.GetAll();
            if (clientes == null || clientes.Count == 0)  //list.Any() ou list.Count verificam objetos como listas
            {
                return NotFound();
            }

            return Ok( new { clientes } );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Find(int id) 
        { 
            var cliente = await _clienteService.Find(id);
            if (cliente.Success != true)
            {
                return NotFound(new { status = cliente.Success, mensagem = cliente.Message });
            }

            return Ok( new { cliente } );
        }

        [HttpGet("by-email")]
        public async Task<IActionResult> FindByEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return BadRequest(new { message = "Email inválido." });
            }

            var cliente = await _clienteService.FindByEmail(email);
            if (cliente.Success != true)
            {
                return NotFound( new { status = cliente.Success, mensagem = cliente.Message } );
            }

            return Ok(new { cliente });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ClienteDto clienteDto)
        {
            if (clienteDto == null)
            {
                return BadRequest();
            }

            var cliente = await _clienteService.Add(clienteDto);
            if (cliente.Success != true)
            {
                return NotFound(new { status = cliente.Success, mensagem = cliente.Message });
            }

            return Ok(new { cliente });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClienteDto clienteDto)
        {
            if (clienteDto == null)
            {
                return BadRequest();
            }

            var cliente = await _clienteService.Update(id, clienteDto);
            if (cliente.Success != true)
            {
                return NotFound(new { status = cliente.Success, mensagem = cliente.Message });
            }

            return Ok(new { cliente });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _clienteService.Delete(id);
            if (cliente.Success != true)
            {
                return NotFound(new { status = cliente.Success, mensagem = cliente.Message });
            }

            return Ok(new { status = cliente.Success, mensagem = cliente.Message });
        }
    }
}

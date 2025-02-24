using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedido10.Application.Contract;
using Pedido10.Application.Formater;
using Pedido10.Application.Service;
using Pedido10.Application.Validator;
using Pedido10.Data.Context;
using Pedido10.Domain.Dto;
using System.Text.RegularExpressions;

namespace Pedido10.API.Controllers
{
    [Controller]
    [Route("api/[Controller]")]
    public class FormaPgtoController(IFormaPgtoService formaPgtoService) : Controller
    {
        private readonly IFormaPgtoService _formaPgtoService = formaPgtoService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var formaPgtos = await _formaPgtoService.GetAll();
            if (formaPgtos == null || formaPgtos.Count == 0)  //list.Any() ou list.Count verificam objetos como listas
            {
                return NotFound(new { resultado = "Nenhuma Formas de Pagamento Cadastrada" });
            }

            return Ok(new { success = true, message = "Formas de Pagamento encontradas", result = formaPgtos });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Find(int id)
        {
            var formaPgto = await _formaPgtoService.Find(id);
            if (formaPgto.Success != true)
            {
                return NotFound(new { status = formaPgto.Success, mensagem = formaPgto.Message });
            }

            return Ok(new { formaPgto });
        }

        [HttpGet("by-name")]
        public async Task<IActionResult> FindByName([FromQuery] string descFormaPgto)
        {
            var formaPgto = await _formaPgtoService.FindByName(descFormaPgto);
            if (formaPgto.Success != true)
            {
                return NotFound(new { status = formaPgto.Success, mensagem = formaPgto.Message });
            }

            return Ok(new { formaPgto });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Forma_PGTO_Dto formaPgtoDto)
        {
            if (string.IsNullOrWhiteSpace(formaPgtoDto.Desc_Forma_PGTO))
            {
                return BadRequest(new { message = "Forma de Pagamento inválido." });
            }
            var formaPgto = await _formaPgtoService.Add(formaPgtoDto);
            if (formaPgto.Success != true)
            {
                return NotFound(new { status = formaPgto.Success, mensagem = formaPgto.Message });
            }

            return Ok(new { formaPgto });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Forma_PGTO_Dto formaPgtoDto)
        {
            if (string.IsNullOrEmpty(formaPgtoDto.Desc_Forma_PGTO) || id == null)
            {
                return BadRequest(new { message = "Froma de Pagamento inválido." });
            }

            var formaPgto = await _formaPgtoService.Update(id, formaPgtoDto);
            if (formaPgto.Success != true)
            {
                return NotFound(new { status = formaPgto.Success, mensagem = formaPgto.Message });
            }

            return Ok(new { formaPgto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var formaPgto = await _formaPgtoService.Delete(id);
            if (formaPgto.Success != true)
            {
                return NotFound(new { status = formaPgto.Success, mensagem = formaPgto.Message });
            }

            return Ok(new { status = formaPgto.Success, mensagem = formaPgto.Message });
        }
    }
}

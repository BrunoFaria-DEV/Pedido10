using Microsoft.AspNetCore.Mvc;
using Pedido10.Domain.Entity;
using Pedido10.Data;
using Pedido10.Data.Context;
using Pedido10.Application.Service;
using Pedido10.Application.Contract;
using Pedido10.Domain.Dto;
using System.Text.RegularExpressions;

namespace Pedido10.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await _produtoService.GetAll();
            if (produtos == null)
            {
                return NotFound( new { resultado = "Nenhum produto Cadastrado" } );
            }

            return Ok( new { sucesses = true, message = "Produtos encontrados", result = produtos } );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Find(int id)
        {
            var produto = await _produtoService.Find(id);
            if (produto.Success != true)
            {
                return NotFound(new { status = produto.Success, mensagem = produto.Message });
            }

            return Ok(new { produto });
        }

        [HttpGet("by-name")]
        public async Task<IActionResult> FindByEmail([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(new { message = "Por favor, digite o nome para pesquisar." });
            }

            var produto = await _produtoService.FindByName(name);
            if (produto.Success != true)
            {
                return NotFound(new { status = produto.Success, mensagem = produto.Message });
            }

            return Ok(new { produto });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProdutoDto produtoDto)
        {
            if (produtoDto == null)
            {
                return BadRequest();
            }

            var produto = await _produtoService.Add(produtoDto);
            if (produto.Success != true)
            {
                return NotFound(new { status = produto.Success, mensagem = produto.Message });
            }

            return Ok(new { produto });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProdutoDto produtoDto)
        {
            if (produtoDto == null)
            {
                return BadRequest();
            }

            var produto = await _produtoService.Update(id, produtoDto);
            if (produto.Success != true)
            {
                return NotFound(new { status = produto.Success, mensagem = produto.Message });
            }

            return Ok(new { produto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _produtoService.Delete(id);
            if (produto.Success != true)
            {
                return NotFound(new { status = produto.Success, mensagem = produto.Message });
            }

            return Ok(new { status = produto.Success, mensagem = produto.Message });
        }

    }
}

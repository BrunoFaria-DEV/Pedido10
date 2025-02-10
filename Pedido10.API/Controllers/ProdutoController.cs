using Microsoft.AspNetCore.Mvc;
using Pedido10.Domain.Entity;
using Pedido10.Data;
using Pedido10.Data.Context;
using Pedido10.Application.Service;
using Pedido10.Application.Contract;
using Pedido10.Domain.Dto;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using Pedido10.Application.Validator;

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
        public async Task<IActionResult> Add(CreateProdutoValidator validator, [FromBody] ProdutoDto produtoDto)
        {
            var validateResult = await validator.ValidateAsync(produtoDto);
            //var error = validateResult.Errors.Select(e => e.ErrorMessage);
            var errors = validateResult.Errors
            .GroupBy(e => e.PropertyName)  // Agrupa os erros pelo nome do campo
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToList()
            );
            if (!validateResult.IsValid)
            {
                return BadRequest( new { errors } );
            }

            var produto = await _produtoService.Add(produtoDto);
            if (produto.Success != true)
            {
                return NotFound(new { status = produto.Success, mensagem = produto.Message });
            }

            return Ok(new { produto });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UpdateProdutoValidator validator, int id, [FromBody] ProdutoDto produtoDto)
        {
            produtoDto.ID_Produto = id;
            var validateResult = await validator.ValidateAsync(produtoDto);
            //var errors = validateResult.Errors.Select(e => e.ErrorMessage);
            var errors = validateResult.Errors
            .GroupBy(e => e.PropertyName)  // Agrupa os erros pelo nome do campo
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToList()
            );
            if (!validateResult.IsValid)
            {
                return BadRequest( new { errors } );
            }
            produtoDto.ID_Produto = null; // usado até conseguir inserir outro parametro como o id no validator

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

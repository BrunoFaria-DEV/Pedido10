using Pedido10.Application.Contract;
using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pedido10.Application.Contract.Auth;
using Pedido10.API.Configuration;
using Microsoft.Extensions.Options;
using Pedido10.Application.Validator;
using Pedido10.Application.Service;
using System.ComponentModel.DataAnnotations;

namespace Pedido10.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController(IUsuarioService usuarioService) : ControllerBase
    {
        private readonly IUsuarioService _usuarioService = usuarioService;

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDto usuarioDto, [FromServices] IAuthService authService)
        {
            var usuario = await _usuarioService.Login(usuarioDto);

            if (usuario == null)
            {
                return Unauthorized(new { sucesso = false, mensagem = "Usuario ou senha inválidos." });
            }

            var token = authService.GenerateJwtToken(usuario);

            return Ok(new { sucesso = true, token = token, usuario = usuario });
        }

        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAll();
            if (usuarios == null)
            {
                return NotFound(new { resultado = "Nenhum usuario Cadastrado" });
            }

            return Ok(new { sucesses = true, message = "Usuarios encontrados", result = usuarios });
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateUsuarioValidator validator, [FromBody]UsuarioDto dto)
        {
            var validateResult = await validator.ValidateAsync(dto);
            var error = validateResult.Errors.Select(e => e.ErrorMessage);
            if (!validateResult.IsValid)
            {
                return BadRequest(error);
            }

            var usuario = await _usuarioService.Add(dto);
            if (usuario.Success != true)
            {
                return NotFound(new { status = usuario.Success, mensagem = usuario.Message });
            }

            return Ok(new { usuario });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UpdateUsuarioValidator validator, int id, [FromBody] UsuarioDto usuarioDto)
        {
            usuarioDto.ID_Usuario = id; // usado até conseguir inserir outro parametro como o id no validator
            var validateResult = await validator.ValidateAsync(usuarioDto);
            var error = validateResult.Errors.Select(e => e.ErrorMessage);
            if (!validateResult.IsValid)
            {
                return BadRequest(error);
            }
            usuarioDto.ID_Usuario = null;

            var usuario = await _usuarioService.Update(id, usuarioDto);
            if (usuario.Success != true)
            {
                return NotFound(new { status = usuario.Success, mensagem = usuario.Message });
            }

            return Ok(new { usuario });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _usuarioService.Delete(id);
            if (usuario.Success != true)
            {
                return NotFound(new { status = usuario.Success, mensagem = usuario.Message });
            }

            return Ok(new { status = usuario.Success, mensagem = usuario.Message });
        }

    }
}

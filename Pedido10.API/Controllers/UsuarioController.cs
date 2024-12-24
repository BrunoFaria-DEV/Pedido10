using Pedido10.Application.Contract;
using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

            //if (usuario == null)
            //{
            //    return
            //}

            var token = authService.GenerateJwtToken(usuario);
            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAll();

            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UsuarioDto dto)
        {
            var retorno = await _usuarioService.Add(dto);
            return Ok(dto);
        }

    }
}

using Pedido10.Application.Contract;
using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Pedido10.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController(IUsuarioService usuarioService) : ControllerBase
    {
        private readonly IUsuarioService _usuarioService = usuarioService;

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAll();

            return Ok(usuarios);
        }

        //[HttpGet("{id:int}")]
        //public IActionResult Get([FromQuery]UsuarioDto teste) {
        //    var testecompleto = teste;
        //    return Ok(testecompleto);
        //}

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UsuarioDto dto)
        {
            var retorno = await _usuarioService.Add(dto);
            return Ok(dto);
        }

    }
}

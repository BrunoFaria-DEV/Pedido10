using Microsoft.AspNetCore.Mvc;
using Pedido10.Application.Contract;

namespace Pedido10.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeService _cidadeService;
        public CidadeController(ICidadeService cidadeService)
        {
            _cidadeService = cidadeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cidades = await _cidadeService.GetAll();
            if (cidades == null)
            {
                return NotFound(new { resultado = "Nenhuma cidade Cadastrada" });
            }

            return Ok(new { success = true, message = "Cidades encontradas", result = cidades });
        }
    }
}

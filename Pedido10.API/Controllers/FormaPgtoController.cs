using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedido10.Data.Context;

namespace Pedido10.API.Controllers
{
    [Controller]
    [Route("api/[Controller]")]
    public class FormaPgtoController(Pedido10Context context) : Controller
    {
        private readonly Pedido10Context _context = context;

        [HttpGet("frm_pgto")]
        public async Task<IActionResult> Fmr_Pgto()
        {
            var result = await _context.Forma_PGTO.ToListAsync();
            return Ok(result);
        }
    }
}

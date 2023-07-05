using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BANDPAR_RestAPI.DataModels;

namespace BANDPAR_RestAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("vendas")]

        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto)
        {
            var vendas = await contexto.Vendas.AsNoTracking().ToListAsync();

            //*AsNoTracking só pode ser utilizado em consultas, declarando que não terá creates ou updates, recomendado por questões de desempenho*

            return vendas == null ? NotFound() : Ok(vendas);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("vendas/{codigoVenda}")]

        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto, [FromRoute] int codigoVenda)
        {
            var vendas = await contexto.Vendas.AsNoTracking().Where(v => v.codigoVenda == codigoVenda).ToListAsync();

            return vendas == null ? NotFound() : Ok(vendas);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("vendas")]
        public async Task<IActionResult> PostAsync([FromServices] Contexto contexto, [FromBody] Vendas venda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await contexto.Vendas.AddAsync(venda);
                await contexto.SaveChangesAsync();
                return Created($"Vendas/vendas/{venda.codigoVenda}", venda);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Microsoft.AspNetCore.Mvc.Route("vendas/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] Contexto contexto, [FromRoute] int id)
        {
            var v = await contexto.Vendas.FirstOrDefaultAsync(v => v.Id == id);

            if (v == null)
            {
                return NotFound("Produto não encontrado!");
            }

            try
            {
                contexto.Vendas.Remove(v);
                await contexto.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

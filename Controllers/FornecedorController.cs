using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BANDPAR_RestAPI.DataModels;

namespace BANDPAR_RestAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("fornecedor")]

        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto)
        {
            //var produtos = await contexto.Produtos.ToListAsync();
            var fornecedor = await contexto.Fornecedor.AsNoTracking().ToListAsync();

            //*AsNoTracking só pode ser utilizado em consultas, declarando que não terá creates ou updates, recomendado por questões de desempenho*

            return fornecedor == null ? NotFound() : Ok(fornecedor);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("fornecedor/{nome}")]

        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto, [FromRoute] string nome)
        {
            //var produtos = await contexto.Produtos.ToListAsync();
            var fornecedor = await contexto.Fornecedor.AsNoTracking().FirstOrDefaultAsync(f => f.nome == nome);

            //*AsNoTracking só pode ser utilizado em consultas, declarando que não terá creates ou updates, recomendado por questões de desempenho*

            return fornecedor == null ? NotFound() : Ok(fornecedor);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("fornecedor/{id}")]

        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto, [FromRoute] int id)
        {
            //var produtos = await contexto.Produtos.ToListAsync();
            var fornecedor = await contexto.Fornecedor.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

            //*AsNoTracking só pode ser utilizado em consultas, declarando que não terá creates ou updates, recomendado por questões de desempenho*

            return fornecedor == null ? NotFound() : Ok(fornecedor);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("fornecedor")]
        public async Task<IActionResult> PostAsync([FromServices] Contexto contexto, [FromBody] Fornecedor fornecedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await contexto.Fornecedor.AddAsync(fornecedor);
                await contexto.SaveChangesAsync();
                return Created($"Fornecedor/fornecedor/{fornecedor.Id}", fornecedor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Microsoft.AspNetCore.Mvc.Route("fornecedor/{id}")]
        public async Task<IActionResult> PutAsync([FromServices] Contexto contexto, [FromBody] Fornecedor fornecedor, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model inválida");
            }

            var f = await contexto.Fornecedor.FirstOrDefaultAsync(f => f.Id == id);

            if (f == null)
            {
                return NotFound("Fornecedor não encontrada!");
            }

            try
            {
                f.nome = fornecedor.nome;

                contexto.Fornecedor.Update(f);
                await contexto.SaveChangesAsync();
                return Ok(f);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Microsoft.AspNetCore.Mvc.Route("fornecedor/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] Contexto contexto, [FromRoute] int id)
        {
            var f = await contexto.Fornecedor.FirstOrDefaultAsync(f => f.Id == id);

            if (f == null)
            {
                return NotFound("Fornecedor não encontrado!");
            }

            try
            {
                contexto.Fornecedor.Remove(f);
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

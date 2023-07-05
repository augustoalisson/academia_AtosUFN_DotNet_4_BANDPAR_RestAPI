using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BANDPAR_RestAPI.DataModels;

namespace BANDPAR_RestAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("produto")]

        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto)
        {
            //var produtos = await contexto.Produtos.ToListAsync();
            var produtos = await contexto.Produtos.AsNoTracking().ToListAsync();

            //*AsNoTracking só pode ser utilizado em consultas, declarando que não terá creates ou updates, recomendado por questões de desempenho*

            return produtos == null ? NotFound() : Ok(produtos);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("produto/{codigo}")]

        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto, [FromRoute] int codigo)
        {
            //var produtos = await contexto.Produtos.ToListAsync();
            var produtos = await contexto.Produtos.AsNoTracking().Where(p => p.codigo == codigo).ToListAsync();

            //*AsNoTracking só pode ser utilizado em consultas, declarando que não terá creates ou updates, recomendado por questões de desempenho*

            return produtos == null ? NotFound() : Ok(produtos);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("produto")]
        public async Task<IActionResult> PostAsync([FromServices] Contexto contexto, [FromBody] Produtos produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await contexto.Produtos.AddAsync(produto);
                await contexto.SaveChangesAsync();
                return Created($"Produtos/produto/{produto.codigo}", produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Microsoft.AspNetCore.Mvc.Route("produto/{id}")]
        public async Task<IActionResult> PutAsync([FromServices] Contexto contexto, [FromBody] Produtos produto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model inválida");
            }

            var p = await contexto.Produtos.FirstOrDefaultAsync(p => p.Id == id);

            if (p == null)
            {
                return NotFound("Produto não encontrada!");
            }

            try
            {
                p.descricao = produto.descricao;
                p.marca = produto.marca;
                p.fornecedor = produto.fornecedor;
                p.valor = produto.valor;

                contexto.Produtos.Update(p);
                await contexto.SaveChangesAsync();
                return Ok(p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Microsoft.AspNetCore.Mvc.Route("produto/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] Contexto contexto, [FromRoute] int id)
        {
            var p = await contexto.Produtos.FirstOrDefaultAsync(p => p.Id == id);

            if (p == null)
            {
                return NotFound("Produto não encontrado!");
            }

            try
            {
                contexto.Produtos.Remove(p);
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

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BANDPAR_RestAPI.DataModels;

namespace BANDPAR_RestAPI.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("usuarios")]

        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto)
        {
            //var produtos = await contexto.Produtos.ToListAsync();
            var usuarios = await contexto.Usuarios.AsNoTracking().ToListAsync();

            //*AsNoTracking só pode ser utilizado em consultas, declarando que não terá creates ou updates, recomendado por questões de desempenho*

            return usuarios == null ? NotFound() : Ok(usuarios);
        }

        /*
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("usuarios/{nome}")]

        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto, [FromRoute] string nome)
        {
            //var produtos = await contexto.Produtos.ToListAsync();
            var fornecedor = await contexto.Fornecedor.AsNoTracking().FirstOrDefaultAsync(f => f.nome == nome);

            //*AsNoTracking só pode ser utilizado em consultas, declarando que não terá creates ou updates, recomendado por questões de desempenho*

            return fornecedor == null ? NotFound() : Ok(fornecedor);
        }
        */

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("usuarios")]
        public async Task<IActionResult> PostAsync([FromServices] Contexto contexto, [FromBody] Usuarios usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await contexto.Usuarios.AddAsync(usuario);
                await contexto.SaveChangesAsync();
                return Created($"Usuarios/usuarios/{usuario.Id}", usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Microsoft.AspNetCore.Mvc.Route("usuarios/{id}")]
        public async Task<IActionResult> PutAsync([FromServices] Contexto contexto, [FromBody] Usuarios usuario, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model inválida");
            }

            var u = await contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            if (u == null)
            {
                return NotFound("Usuário não encontrada!");
            }

            try
            {
                u.usuario = usuario.usuario;
                u.senha = usuario.senha;

                contexto.Usuarios.Update(u);
                await contexto.SaveChangesAsync();
                return Ok(u);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Microsoft.AspNetCore.Mvc.Route("usuarios/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] Contexto contexto, [FromRoute] int id)
        {
            var u = await contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            if (u == null)
            {
                return NotFound("Usuário não encontrado!");
            }

            try
            {
                contexto.Usuarios.Remove(u);
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

using System;
using System.Linq;
using System.Threading.Tasks;
using FortesApi.Models.Context;
using FortesApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FortesApi.Controllers
{
  [Route("[controller]")]
  [Produces("application/json")]
  [ApiController]
  public class UsuarioController : ControllerBase
  {

    /// <summary>
    /// Incluir usuário.
    /// </summary>
    /// <param name="usuario">Informações da usuário.</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Post([FromBody] Usuario usuario)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return BadRequest(new
          {
            Codigo = BadRequest().StatusCode,
            Mensagem = "Erro na validação da solicitação.",
            Itens = ModelState.SelectMany(x => x.Value.Errors)
                  .Select(x => x.ErrorMessage)
                  .ToList()
          });
        }

        using (var db = new FortesContext())
        {

          await db.Usuario.AddAsync(usuario);
          await db.SaveChangesAsync();

          //OK
          return Created("", new
          {
            Codigo = 201,
            Mensagem = "Registro criado com sucesso!",
          });
        }
      }
      catch (Exception ex)
      {
        return StatusCode(500, new
        {
          Codigo = 500,
          Mensagem = ex.InnerException?.InnerException?.Message ?? ex.Message,
        });
      }
    }
  }
}

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FortesApi.Models.Context;
using FortesApi.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FortesApi.Controllers
{
  /// <inheritdoc />
  /// <summary>
  ///  Compra
  /// </summary> 
  [Route("[controller]")]
  [Produces("application/json")]
  [ApiController]
  [Authorize]
  public class CompraController : ControllerBase
  {
    /// <summary>
    /// Obter lista de compras.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<Compra>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        using (var db = new FortesContext())
        {
          var item = await db.Compra
          .Where(x => x.UsuarioKey.ToString() == HttpContext.User.Identity.Name)
          .ToListAsync();

          if (item == null)
            return NotFound(new
            {
              Codigo = NotFound().StatusCode,
              Mensagem = "Nenhum registro encontrado.",
            });

          return Ok(item);
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

    /// <summary>
    /// Obter uma compra especifíca chave de identificação.
    /// </summary>
    /// <param name="key">Chave de identificação da compra.</param>
    /// <returns></returns>
    [HttpGet("{key}")]
    [ProducesResponseType(typeof(Compra), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [AllowAnonymous]
    public async Task<IActionResult> GetByKey(Guid key)
    {
      try
      {
        using (var db = new FortesContext())
        {
          var item = await db.Compra
              .Where(x => x.UsuarioKey.ToString() == HttpContext.User.Identity.Name)
              .Where(x => x.CompraKey == key)
              .FirstOrDefaultAsync();

          if (item == null)
            return NotFound(new
            {
              Codigo = NotFound().StatusCode,
              Mensagem = "Nenhum registro encontrado.",
            });

          return Ok(item);
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


    /// <summary>
    /// Incluir compra.
    /// </summary>
    /// <param name="compra">Informações da compra.</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Post([FromBody] Compra compra)
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

        compra.DataCompra = compra.DataCompra ?? DateTime.Now;
        compra.UsuarioKey = Guid.Parse(HttpContext.User.Identity.Name);

        using (var db = new FortesContext())
        {

          await db.Compra.AddAsync(compra);
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
          ex
        });
      }
    }

    /// <summary>
    /// Alterar compra.
    /// </summary>
    /// <param name="key">Chave de identificação da compra.</param>
    /// <param name="categoria">Informações da compra.</param>
    /// <returns></returns>
    [HttpPut("{key}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(Guid key, [FromBody] Compra compra)
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
          var item = await db.Compra
              .Where(x => x.UsuarioKey.ToString() == HttpContext.User.Identity.Name)
              .Where(x => x.CompraKey == key)
              .FirstOrDefaultAsync();

          if (item == null)
            return NotFound(new
            {
              Codigo = NotFound().StatusCode,
              Mensagem = "Nenhum registro encontrado.",
            });

          item.Descricao = compra.Descricao;
          item.Valor = compra.Valor;
          item.TaxaJuros = compra.TaxaJuros;
          item.ValorTotal = compra.ValorTotal;
          item.QuantidadeParcelas = compra.QuantidadeParcelas;
          item.DataCompra = compra.DataCompra ?? item.DataCompra;

          db.Update(item);
          await db.SaveChangesAsync();

          return Ok(new
          {
            Codigo = Ok().StatusCode,
            Mensagem = "Registro atualizado com sucesso!",
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

    /// <summary>
    /// Excluir compra.
    /// </summary>
    /// <param name="key">Chave de identificação da compra.</param>
    /// <returns></returns>
    [HttpDelete("{key}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(Guid key)
    {
      try
      {
        using (var db = new FortesContext())
        {
          var item = await db.Compra
              .Where(x => x.UsuarioKey.ToString() == HttpContext.User.Identity.Name)
              .Where(x => x.CompraKey == key)
              .FirstOrDefaultAsync();

          if (item == null)
            return NotFound(new
            {
              Codigo = NotFound().StatusCode,
              Mensagem = "Nenhum registro encontrado.",
            });

          db.Remove(item);
          await db.SaveChangesAsync();

          return Ok(new
          {
            Codigo = Ok().StatusCode,
            Mensagem = "Registro excluído com sucesso!",
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
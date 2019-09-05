using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Fortes.Security;
using FortesApi.Models.Context;
using FortesApi.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FortesApi.Controllers
{
  [Route("[controller]")]
  [ApiController]
  [ApiExplorerSettings(IgnoreApi = true)]
  public class LoginController : ControllerBase
  {
    private readonly JwtTokenOptions _tokenOptions;

    /// <inheritdoc />
    public LoginController(IOptions<JwtTokenOptions> jwtOptions) => _tokenOptions = jwtOptions.Value;

    // POST api/values
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Post([FromBody] Login login)
    {
      if (login == null)
        return BadRequest(new
        {
          Codigo = BadRequest().StatusCode,
          Mensagem = "Usuário ou senha inválidos.",
        });

      using (var db = new FortesContext())
      {
        try
        {
          //busca o usuario no banco 
          var item = await db.Usuario.FirstOrDefaultAsync(x =>
              x.Email.Equals(login.Email) && x.Password.Equals(login.Password));

          if (item == null)
            return NotFound(new
            {
              Codigo = NotFound().StatusCode,
              Mensagem = "Usuário ou senha inválidos.",
            });

          var claims = new[]
          {
            new Claim(JwtRegisteredClaimNames.UniqueName, item.UsuarioKey.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, _tokenOptions.ToUnixEpochDate().ToString(),
                            ClaimValueTypes.Integer64),
            //new Claim(JwtRegisteredClaimNames.Sub, item.UsuarioKey.ToString()),
            new Claim(ClaimTypes.Name, item.UsuarioKey.ToString())
          };

          var token = new JwtSecurityToken(
              _tokenOptions.Issuer,
              _tokenOptions.Audience,
              claims,
              expires: _tokenOptions.Expiration,
              signingCredentials: _tokenOptions.SigningCredentials
          );

          var response = new
          {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expires = _tokenOptions.Expiration,
            user = new
            {
              key = item.UsuarioKey,
              nome = item.Nome
            }
          };

          return Ok(response);
        }
        catch (Exception ex)
        {
          return BadRequest(ex);
        }
      }
    }
  }
}

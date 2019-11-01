using System.ComponentModel.DataAnnotations;

namespace FortesApi.Models.Entities
{
  public class Login
  {
    /// <summary>
    /// E-mail do usuário.
    /// </summary>
    [StringLength(250, MinimumLength = 3, ErrorMessage = "E-mail deve ter no mínimo 3 caractres e no máximo 250.")]
    [EmailAddress(ErrorMessage = "E-mail no formato inválido")]
    [Required(ErrorMessage = "E-mail é obrigatório.")] public string Email { get; set; }

    /// <summary>
    /// Senha do usuário.
    /// </summary>
    [StringLength(8, MinimumLength = 3, ErrorMessage = "Senha deve ter no mínimo 3 caractres e no máximo 8.")]
    [Required(ErrorMessage = "Senha é obrigatória.")]
    public string Password { get; set; }
  }
}
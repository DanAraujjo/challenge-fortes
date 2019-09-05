using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FortesApi.Models.Entities
{
  /// <summary>
  /// Usuário.
  /// </summary>

  public class Usuario
  {

    public Usuario()
    {
      Compras = new HashSet<Compra>();
    }


    /// <summary>
    /// Chave de identificação do usuário.
    /// </summary>

    [Key]
    public Guid? UsuarioKey { get; set; }

    /// <summary>
    /// Nome do usuário.
    /// </summary>
    [StringLength(250, MinimumLength = 3, ErrorMessage = "Nome deve ter no mínimo 3 caractres e no máximo 250.")]
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }


    /// <summary>
    /// E-mail do usuário.
    /// </summary>
    [StringLength(250, MinimumLength = 3, ErrorMessage = "E-mail deve ter no mínimo 3 caractres e no máximo 250.")]
    [EmailAddress(ErrorMessage = "E-mail no formato inválido")]
    [Required(ErrorMessage = "E-mail é obrigatório.")]
    public string Email { get; set; }

    /// <summary>
    /// Senha do usuário.
    /// </summary>
    [StringLength(8, MinimumLength = 3, ErrorMessage = "Senha deve ter no mínimo 3 caractres e no máximo 8.")]
    [Required(ErrorMessage = "Senha é obrigatória.")]
    public string Password { get; set; }

    /// <summary>
    /// Compras do usuário.
    /// </summary>

    [IgnoreDataMember]
    public virtual ICollection<Compra> Compras { get; set; }
  }
}
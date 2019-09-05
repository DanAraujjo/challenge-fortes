using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FortesApi.Models.Entities
{
  /// <summary>
  /// Compra.
  /// </summary>
  public class Compra
  {
    /// <summary>
    /// Chave de identificação da compra.
    /// </summary>
    [Key]
    public Guid? CompraKey { get; set; }

    /// <summary>
    /// Data compra.
    /// </summary>
    public DateTime? DataCompra { get; set; }

    /// <summary>
    /// Descrição da compra.
    /// </summary>
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Descrição deve ter no mínimo 3 caractres e no máximo 100.")]
    [Required(ErrorMessage = "Descrição é obrigatória.")]
    public string Descricao { get; set; }

    /// <summary>
    /// Valor da compra.
    /// </summary>
    [Required(ErrorMessage = "Valor é obrigatório.")]
    [Range(0.01, Double.MaxValue, ErrorMessage = "O valor deve ser no minímo 0.01")]
    public decimal Valor { get; set; }

    /// <summary>
    /// Taxa de juros.
    /// </summary>
    [Required(ErrorMessage = "Taxa de juros é obrigatório.")]
    [Range(0.0001, Double.MaxValue, ErrorMessage = "A taxa de juros deve ser no minímo 0.0001")]
    public decimal TaxaJuros { get; set; }

    /// <summary>
    /// Quantidade de parcelas.
    /// </summary>
    [Required(ErrorMessage = "Quantidade de parcelas é obrigatório.")]
    [Range(1, 12, ErrorMessage = "A quantidade minima de parcelas é 1 e a máxima é 12.")]
    public int QuantidadeParcelas { get; set; }

    /// <summary>
    /// Valor da compra.
    /// </summary>
    [Required(ErrorMessage = "Valor total é obrigatório.")]
    [Range(0.01, Double.MaxValue, ErrorMessage = "O valor total deve ser no minímo 0.01")]
    public decimal ValorTotal { get; set; }

    [IgnoreDataMember]
    public Guid UsuarioKey { get; set; }

    [IgnoreDataMember]
    public virtual Usuario Usuario { get; set; }
  }
}
using FortesApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FortesApi.Models.Maps
{
  /// <inheritdoc />
  public class CompraMap : IClassMap
  {
    /// <inheritdoc />
    public void Map(ModelBuilder modelBuilder) => modelBuilder.Entity<Compra>(e =>
    {
      // Primary Key
      e.HasKey(x => x.CompraKey);

      // Table & Column Mappings
      e.ToTable("TB_Compras");
      e.Property(x => x.CompraKey).HasColumnName("key").HasDefaultValue();
      e.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(100);
      e.Property(x => x.DataCompra).HasColumnName("data_compra").HasColumnType("datetime");
      e.Property(x => x.Valor).HasColumnName("valor").HasColumnType("decimal(18,2)");
      e.Property(x => x.TaxaJuros).HasColumnName("taxa_juros").HasColumnType("decimal(18,4)");
      e.Property(x => x.QuantidadeParcelas).HasColumnName("quantidade_parcela");
      e.Property(x => x.ValorTotal).HasColumnName("valor_total").HasColumnType("decimal(18,2)");
      e.Property(x => x.UsuarioKey).HasColumnName("usuario_key");

      // Relationships
      e.HasOne(x => x.Usuario).WithMany(x => x.Compras).HasForeignKey(x => x.UsuarioKey);
    });
  }
}

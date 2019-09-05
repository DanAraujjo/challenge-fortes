using FortesApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FortesApi.Models.Maps
{
  /// <inheritdoc />
  public class UsuarioMap : IClassMap
  {
    /// <inheritdoc />
    public void Map(ModelBuilder modelBuilder) => modelBuilder.Entity<Usuario>(e =>
    {
      // Primary Key
      e.HasKey(x => x.UsuarioKey);

      // Table & Column Mappings
      e.ToTable("TB_Usuario");
      e.Property(x => x.UsuarioKey).HasColumnName("key").HasDefaultValue();
      e.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(250);
      e.Property(x => x.Email).HasColumnName("email").HasMaxLength(250);
      e.Property(x => x.Password).HasColumnName("password").HasMaxLength(8);
    });
  }
}

using System.IO;
using FortesApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FortesApi.Models.Maps;

namespace FortesApi.Models.Context
{
  /// <inheritdoc />
  /// <summary />
  public class FortesContext : DbContext
  {
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      //get the configuration from the app settings
      var config = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", false, true)
          .Build();

      // define the database to use
      optionsBuilder.UseSqlServer(config.GetConnectionString("FortesDb"));
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      new UsuarioMap().Map(modelBuilder);
      new CompraMap().Map(modelBuilder);
    }

    /// <summary />
    public DbSet<Usuario> Usuario { get; set; }

    /// <summary />
    public DbSet<Compra> Compra { get; set; }
  }
}
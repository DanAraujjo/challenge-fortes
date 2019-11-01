using Microsoft.EntityFrameworkCore;

namespace FortesApi.Models.Maps
{
  /// <summary />
  public interface IClassMap
  {
    /// <summary />
    void Map(ModelBuilder modelBuilder);
  }
}
using Microsoft.EntityFrameworkCore;

namespace meowtown_api.Models{

  public class MeowtownContext : DbContext {
    public MeowtownContext(DbContextOptions<MeowtownContext> options)
      : base(options){}

    public DbSet<Cat> Cats {get; set;}

  }

}

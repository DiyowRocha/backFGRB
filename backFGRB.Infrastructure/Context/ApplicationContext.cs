using Microsoft.EntityFrameworkCore;

namespace backFGRB.Infrastructure.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        //public DbSet<Entity> Entities { get; set; }
    }
}
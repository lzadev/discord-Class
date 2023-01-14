using Microsoft.EntityFrameworkCore;
using RestaurantAPI.EfCore;
using RestaurantAPI.Entites;

namespace RestaurantAPI;

public class ApplicationContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Category>(new CategoryEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
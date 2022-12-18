using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Data;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<Product> Products => Set<Product>();

    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        BuildCartItems(modelBuilder);
    }

    private void BuildCartItems(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>(action =>
        {
            action.HasOne(dto => dto.Cart)
                .WithMany(dto => dto.Items)
                .IsRequired();
        });
    }
}
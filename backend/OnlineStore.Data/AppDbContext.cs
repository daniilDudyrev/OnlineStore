using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Conveters;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Data;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<ParentCategory> ParentCategories => Set<ParentCategory>();

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
        BuildOrderItems(modelBuilder);
        BuildProductModel(modelBuilder);
        BuildCategoryModel(modelBuilder);
        BuildAccountModel(modelBuilder);
    }

    private void BuildAccountModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .Property(p => p.Roles)
            .HasConversion<RolesValueConverter>();
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

    private void BuildOrderItems(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderItem>(action =>
        {
            action.HasOne(dto => dto.Order)
                .WithMany(dto => dto.Items)
                .IsRequired();
        });
    }

    private void BuildProductModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne<Category>()
            .WithMany()
            .HasForeignKey(dto => dto.CategoryId);
    }

    private void BuildCategoryModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasOne<ParentCategory>()
            .WithMany()
            .HasForeignKey(dto => dto.ParentId);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Data.Config;

public class CartEntityTypeConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> conf)
    {
        conf.ToTable("carts");
        conf.HasKey(it => it.Id);
        conf.Property<string>("Description").IsRequired(false);

        var navigation = conf.Metadata.FindNavigation(nameof(Cart.Items));
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        conf.Navigation(it => it.Items).AutoInclude();
    }
}
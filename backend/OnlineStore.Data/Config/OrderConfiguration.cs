using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Data.Config;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> conf)
    {
        conf.HasKey(it => it.Id);
        conf.Property<string>("Description").IsRequired(false);
        
        var navigation = conf.Metadata.FindNavigation(nameof(Order.Items));
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        conf.Navigation(it => it.Items).AutoInclude();
    }
}
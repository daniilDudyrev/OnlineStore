using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.Test;

public class CartTests
{
    [Fact]
    private void New_item_is_added_to_cart()
    {
        var cart = new Cart(Guid.Empty, Guid.Empty, new List<CartItem>());
        var product = new Product(Guid.Empty, "fake", 50, "img", "desc", Guid.Empty);
        var quantity = 1;

        cart.Add(product, 1);
        var cartItem = cart.Items.First();

        Assert.NotNull(cartItem);
        Assert.Single(cart.Items);
        Assert.Equal(product.Id, cartItem.ProductId);
        Assert.Equal(quantity, cartItem.Quantity);
        Assert.Equal(product.Price, cartItem.Price);
    }

    [Fact]
    private void Adding_existed_product_in_cart_changes_item_quantity()
    {
        var cart = new Cart(Guid.Empty, Guid.Empty, new List<CartItem>());
        var product = new Product(Guid.Empty, "fake", 50, "img", "desc", Guid.Empty);
        var quantity = 2;

        cart.Add(product, 1);
        cart.Add(product, 1);
        var cartItem = cart.Items.First();

        Assert.NotNull(cartItem);
        Assert.Single(cart.Items);
        Assert.Equal(quantity, cartItem.Quantity);
    }

    [Fact]
    private void Adding_empty_product_is_impossible()
    {
        var cart = new Cart(Guid.Empty, Guid.Empty, new List<CartItem>());
        Assert.Throws<ArgumentNullException>(() => cart.Add(null, 1));
    }
}
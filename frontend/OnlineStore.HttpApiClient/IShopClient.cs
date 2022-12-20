using OnlineStore.Domain.Entities;
using OnlineStore.Models.Requests;
using OnlineStore.Models.Responses;

namespace OnlineStore.HttpApiClient;

public interface IShopClient
{
    Task<ProductsResponse> GetProducts(CancellationToken cts = default);
    Task<ProductResponse> GetProduct(Guid id, CancellationToken cts = default);
    Task AddProduct(Product product, CancellationToken cts = default);
    Task UpdateProduct(Guid id, Product product, CancellationToken cts = default);
    Task DeleteProductById(Guid id, CancellationToken cts = default);
    Task<RegisterResponse> Register(RegisterRequest registerRequest, CancellationToken cts = default);
    Task<AuthResponse> Authentication(AuthRequest authRequest, CancellationToken cts = default);
    void SetAuthToken(string token);
    void ResetAuthToken();
    Task<Account> GetAccount(CancellationToken cts = default);
    Task<CartResponse> GetItemsInCart(CancellationToken cts = default);
    Task<CartItemResponse> AddToCart(Guid productId, CancellationToken cts = default);
}
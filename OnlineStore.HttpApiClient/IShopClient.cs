using OnlineStore.Domain;
using OnlineStore.Models.Requests;

namespace OnlineStore.HttpApiClient;

public interface IShopClient
{
    Task<IReadOnlyList<Product>> GetProducts(CancellationToken cts = default);
    Task<Product> GetProduct(Guid id, CancellationToken cts = default);
    Task AddProduct(Product product, CancellationToken cts = default);
    Task UpdateProduct(Guid id, Product product, CancellationToken cts = default);
    Task DeleteProductById(Guid id, CancellationToken cts = default);
    Task Register(RegisterRequest request, CancellationToken cts = default);
}
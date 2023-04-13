using OnlineStore.Domain.Entities;
using OnlineStore.Models.Requests;
using OnlineStore.Models.Responses;

namespace OnlineStore.HttpApiClient;

public interface IShopClient
{
    Task<ProductsResponse> GetProducts(CancellationToken cancellationToken = default);
    Task<ProductsResponse> GetProductsByCategoryId(Guid categoryId, CancellationToken cancellationToken = default);

    Task<ProductResponse> GetProduct(Guid id, CancellationToken cancellationToken = default);
    Task AddProduct(Product product, CancellationToken cancellationToken = default);
    Task UpdateProduct(Guid id, Product product, CancellationToken cancellationToken = default);
    Task DeleteProductById(Guid id, CancellationToken cancellationToken = default);
    Task<RegisterResponse> Register(RegisterRequest registerRequest, CancellationToken cancellationToken = default);
    Task<AuthResponse> Authentication(AuthRequest authRequest, CancellationToken cancellationToken = default);
    void SetAuthToken(string token);
    void ResetAuthToken();
    Task<Account> GetCurrentAccount(CancellationToken cancellationToken = default);
    Task<CartResponse> GetItemsInCart(CancellationToken cancellationToken = default);
    Task<CartItemResponse> AddToCart(Guid productId, int quantity, CancellationToken cancellationToken = default);
    Task<CategoriesResponse> GetCategories(CancellationToken cancellationToken = default);
    Task<CategoryResponse> GetCategory(Guid id, CancellationToken cancellationToken = default);
    Task AddCategory(CategoryRequest category, CancellationToken cancellationToken = default);
    Task UpdateCategory(Guid id, CategoryRequest category, CancellationToken cancellationToken = default);
    Task DeleteCategoryById(Guid id, CancellationToken cancellationToken = default);
}
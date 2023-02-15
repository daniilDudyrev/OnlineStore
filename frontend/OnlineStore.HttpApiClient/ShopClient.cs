using System.Net.Http.Headers;
using System.Net.Http.Json;
using OnlineStore.Domain.Entities;
using OnlineStore.Models.Requests;
using OnlineStore.Models.Responses;

namespace OnlineStore.HttpApiClient;

public class ShopClient : IShopClient
{
    private const string DefaultHost = "https://api.mysite.com";
    private readonly string _host;
    private readonly HttpClient _httpClient;
    public bool IsAuthorizationTokenSet { get; private set; }

    public ShopClient(string host = DefaultHost, HttpClient? httpClient = null)
    {
        _host = host;
        _httpClient = httpClient ?? new HttpClient();
    }

    public async Task<ProductsResponse> GetProducts(CancellationToken cancellationToken = default)
    {
        var uri = $"{_host}/products/get_all";
        var responseMessage = await _httpClient.GetFromJsonAsync<ProductsResponse>(uri, cancellationToken);
        return responseMessage!;
    }

    public async Task<ProductsResponse> GetProductsByCategoryId(Guid categoryId, CancellationToken cancellationToken = default)
    {
        var uri = $"{_host}/products/get_by_category?categoryId={categoryId}";
        var responseMessage = await _httpClient.GetFromJsonAsync<ProductsResponse>(uri, cancellationToken);
        return responseMessage!;
    }

    public async Task<ProductResponse> GetProduct(Guid id, CancellationToken cancellationToken = default)
    {
        var uri = $"{_host}/products/get_by_id?id={id}";
        var responseMessage = await _httpClient.GetFromJsonAsync<ProductResponse>(uri, cancellationToken);
        return responseMessage!;
    }

    public async Task AddProduct(Product product, CancellationToken cancellationToken = default)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        var uri = $"{_host}/products/add";
        var responseMessage = await _httpClient.PostAsJsonAsync(uri, product, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
    }

    public async Task UpdateProduct(Guid id, Product product, CancellationToken cancellationToken = default)
    {
        var uri = $"{_host}/products/update/{id}";
        var responseMessage = await _httpClient.PutAsJsonAsync(uri, product, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
    }

    public async Task DeleteProductById(Guid id, CancellationToken cancellationToken = default)
    {
        var uri = $"{_host}/products/delete_by_id/{id}";
        var responseMessage = await _httpClient.DeleteAsync(uri, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
    }

    public async Task<RegisterResponse> Register(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var uri = $"{_host}/account/register";
        var responseMessage = await _httpClient.PostAsJsonAsync(uri, request, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<RegisterResponse>(cancellationToken: cancellationToken);
        SetAuthToken(response!.Token!);
        return response;
    }

    public async Task<AuthResponse> Authentication(AuthRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var uri = $"{_host}/account/authentication";
        var responseMessage = await _httpClient.PostAsJsonAsync(uri, request, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<AuthResponse>(cancellationToken: cancellationToken);
        SetAuthToken(response!.Token!);
        return response;
    }

    public async Task<Account> GetAccount(CancellationToken cancellationToken = default)
    {
        var uri = $"{_host}/account/get_current";
        var responseMessage = await _httpClient.GetFromJsonAsync<Account>(uri, cancellationToken);
        return responseMessage!;
    }

    public async Task<CartResponse> GetItemsInCart(CancellationToken cancellationToken = default)
    {
        var uri = $"{_host}/cart/get";
        var responseMessage = await _httpClient.GetFromJsonAsync<CartResponse>(uri, cancellationToken);
        return responseMessage!;
    }

    public async Task<CartItemResponse> AddToCart(Guid productId, CancellationToken cancellationToken = default)
    {
        var uri = $"{_host}/cart/add_item?productId={productId}";
        var responseMessage = await _httpClient.PostAsJsonAsync(uri, productId, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<CartItemResponse>(cancellationToken: cancellationToken);
        return response!;
    }

    public async Task<CategoriesResponse> GetCategories(CancellationToken cancellationToken = default)
    {
        var uri = $"{_host}/categories/get_all";
        var responseMessage = await _httpClient.GetFromJsonAsync<CategoriesResponse>(uri, cancellationToken);
        return responseMessage!;
    }

    public async Task<CategoryResponse> GetCategory(Guid id, CancellationToken cancellationToken = default)
    {
        var uri = $"{_host}/categories/get_by_id?id={id}";
        var responseMessage = await _httpClient.GetFromJsonAsync<CategoryResponse>(uri, cancellationToken);
        return responseMessage!;
    }

    public async Task AddCategory(CategoryRequest category, CancellationToken cancellationToken = default)
    {
        if (category is null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        var uri = $"{_host}/categories/add";
        var responseMessage = await _httpClient.PostAsJsonAsync(uri, category, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
    }

    public async Task UpdateCategory(Guid id, CategoryRequest category, CancellationToken cancellationToken = default)
    {
        var uri = $"{_host}/categories/update/{id}";
        var responseMessage = await _httpClient.PutAsJsonAsync(uri, category, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
    }

    public async Task DeleteCategoryById(Guid id, CancellationToken cancellationToken = default)
    {
        var uri = $"{_host}/categories/delete_by_id/{id}";
        var responseMessage = await _httpClient.DeleteAsync(uri, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
    }

    public void SetAuthToken(string token)
    {
        if (token == null)
        {
            throw new ArgumentNullException(nameof(token));
        }

        var header = new AuthenticationHeaderValue("Bearer", token);
        _httpClient.DefaultRequestHeaders.Authorization = header;
        IsAuthorizationTokenSet = true;
    }

    public void ResetAuthToken()
    {
        _httpClient.DefaultRequestHeaders.Remove("Authorization");
        IsAuthorizationTokenSet = false;
    }
}
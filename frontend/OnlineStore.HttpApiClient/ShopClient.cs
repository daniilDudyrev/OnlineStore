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

    public async Task<ProductsResponse> GetProducts(CancellationToken cts = default)
    {
        var uri = $"{_host}/products/get_all";
        var responseMessage = await _httpClient.GetFromJsonAsync<ProductsResponse>(uri, cts);
        return responseMessage!;
    }

    public async Task<ProductResponse> GetProduct(Guid id, CancellationToken cts = default)
    {
        var uri = $"{_host}/products/get_by_id?id={id}";
        var responseMessage = await _httpClient.GetFromJsonAsync<ProductResponse>(uri, cts);
        return responseMessage!;
    }

    public async Task AddProduct(Product product, CancellationToken cts = default)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        var uri = $"{_host}/products/add";
        var responseMessage = await _httpClient.PostAsJsonAsync(uri, product, cts);
        responseMessage.EnsureSuccessStatusCode();
    }

    public async Task UpdateProduct(Guid id, Product product, CancellationToken cts = default)
    {
        var uri = $"{_host}/products/update/{id}";
        var responseMessage = await _httpClient.PutAsJsonAsync(uri, product, cts);
        responseMessage.EnsureSuccessStatusCode();
    }

    public async Task DeleteProductById(Guid id, CancellationToken cts = default)
    {
        var uri = $"{_host}/products/delete_by_id/{id}";
        var responseMessage = await _httpClient.DeleteAsync(uri, cts);
        responseMessage.EnsureSuccessStatusCode();
    }

    public async Task<RegisterResponse> Register(RegisterRequest request, CancellationToken cts = default)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var uri = $"{_host}/account/register";
        var responseMessage = await _httpClient.PostAsJsonAsync(uri, request, cts);
        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<RegisterResponse>(cancellationToken: cts);
        SetAuthToken(response!.Token);
        return response;
    }

    public async Task<AuthResponse> Authentication(AuthRequest request, CancellationToken cts = default)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var uri = $"{_host}/account/authentication";
        var responseMessage = await _httpClient.PostAsJsonAsync(uri, request, cts);
        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<AuthResponse>(cancellationToken: cts);
        SetAuthToken(response!.Token);
        return response;
    }

    public async Task<Account> GetAccount(CancellationToken cts = default)
    {
        var uri = $"{_host}/account/get_current";
        var responseMessage = await _httpClient.GetFromJsonAsync<Account>(uri, cts);
        return responseMessage!;
    }

    public async Task<CartResponse> GetItemsInCart(CancellationToken cts = default)
    {
        var uri = $"{_host}/cart/get";
        var responseMessage = await _httpClient.GetFromJsonAsync<CartResponse>(uri, cts);
        return responseMessage!;
    }

    public async Task<CartItemResponse> AddToCart(Guid productId, CancellationToken cts = default)
    {
        var uri = $"{_host}/cart/add_item?productId={productId}";
        var responseMessage = await _httpClient.PostAsJsonAsync(uri, productId, cts);
        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<CartItemResponse>(cancellationToken: cts);
        return response!;
    }

    public async Task<CategoriesResponse> GetCategories(CancellationToken cts = default)
    {
        var uri = $"{_host}/categories/get_all";
        var responseMessage = await _httpClient.GetFromJsonAsync<CategoriesResponse>(uri, cts);
        return responseMessage!;
    }

    public async Task<CategoryResponse> GetCategory(Guid id, CancellationToken cts = default)
    {
        var uri = $"{_host}/categories/get_by_id?id={id}";
        var responseMessage = await _httpClient.GetFromJsonAsync<CategoryResponse>(uri, cts);
        return responseMessage!;
    }

    public async Task AddCategory(CategoryRequest category, CancellationToken cts = default)
    {
        if (category is null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        var uri = $"{_host}/categories/add";
        var responseMessage = await _httpClient.PostAsJsonAsync(uri, category, cts);
        responseMessage.EnsureSuccessStatusCode();
    }

    public async Task UpdateCategory(Guid id, CategoryRequest category, CancellationToken cts = default)
    {
        var uri = $"{_host}/categories/update/{id}";
        var responseMessage = await _httpClient.PutAsJsonAsync(uri, category, cts);
        responseMessage.EnsureSuccessStatusCode();
    }

    public async Task DeleteCategoryById(Guid id, CancellationToken cts = default)
    {
        var uri = $"{_host}/categories/delete_by_id/{id}";
        var responseMessage = await _httpClient.DeleteAsync(uri, cts);
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
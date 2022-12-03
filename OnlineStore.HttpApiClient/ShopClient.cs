using System.Net.Http.Json;
using OnlineStore.Domain;
using OnlineStore.Models.Requests;

namespace OnlineStore.HttpApiClient;

public class ShopClient : IShopClient
{
    private const string DefaultHost = "https://api.mysite.com";
    private readonly string _host;
    private readonly HttpClient _httpClient;

    public ShopClient(string host = DefaultHost, HttpClient? httpClient = null)
    {
        _host = host;
        _httpClient = httpClient ?? new HttpClient();
    }

    public async Task<IReadOnlyList<Product>> GetProducts(CancellationToken cts = default)
    {
        var uri = $"{_host}/products/get_all";
        var response = await _httpClient.GetFromJsonAsync<IReadOnlyList<Product>>(uri, cts);
        return response!;
    }

    public async Task<Product> GetProduct(Guid id, CancellationToken cts = default)
    {
        var uri = $"{_host}/products/get_by_id?id={id}";
        var response = await _httpClient.GetFromJsonAsync<Product>(uri, cts);
        return response!;
    }

    public async Task AddProduct(Product product, CancellationToken cts = default)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        var uri = $"{_host}/products/add";
        var response = await _httpClient.PostAsJsonAsync(uri, product, cts);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateProduct(Guid id, Product product, CancellationToken cts = default)
    {
        var uri = $"{_host}/products/update/{id}";
        var response = await _httpClient.PutAsJsonAsync(uri, product, cts);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteProductById(Guid id, CancellationToken cts = default)
    {
        var uri = $"{_host}/products/delete_by_id/{id}";
        var response = await _httpClient.DeleteAsync(uri, cts);
        response.EnsureSuccessStatusCode();
    }
    public async Task Register(RegisterRequest request, CancellationToken cts = default)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var uri = $"{_host}/account/register";
        var response = await _httpClient.PostAsJsonAsync(uri, request, cts);
        response.EnsureSuccessStatusCode();
    }
}
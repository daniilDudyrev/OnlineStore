using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using OnlineStore.HttpApiClient;
#pragma warning disable CS8618

namespace OnlineStore.BlazorClient.Components;

public abstract class AppComponentBase: ComponentBase
{
    [Inject] protected IShopClient ShopClient { get; private set; }
    [Inject] protected ILocalStorageService LocalStorage { get; private set; }

    private bool IsTokenChecked { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if (!IsTokenChecked)
        {
            IsTokenChecked = true;
            var token = await LocalStorage.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token))
            {
                ShopClient.SetAuthToken(token);
            }
        }
    }
}
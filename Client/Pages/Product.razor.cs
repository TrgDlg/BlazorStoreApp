using StoreBlazor.Client.HttpClients;
using StoreBlazor.Client.Models;
using Microsoft.AspNetCore.Components;

namespace StoreBlazor.Client.Pages
{
    public class ProductBase : ComponentBase
    {
        [Inject] ProductsHttpClient ProductsHttpClient { get; init; } = null!;

        protected StoreBlazor.Client.Models.Product Product { get; set; }

        [Parameter]
        public string SearchValue { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Product = await ProductsHttpClient.GetProductByName(SearchValue);

            await base.OnInitializedAsync();
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            Product = await ProductsHttpClient.GetProductByName(SearchValue);
            base.OnAfterRender(firstRender);
        }

    }
}
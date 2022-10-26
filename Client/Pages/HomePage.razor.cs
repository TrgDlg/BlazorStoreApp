using StoreBlazor.Client.HttpClients;
using Microsoft.AspNetCore.Components;
using StoreBlazor.Client.Models;

namespace StoreBlazor.Client.Pages
{
    public class HomePageBase : ComponentBase
    {
        [Inject] ProductsHttpClient ProductsHttpClient { get; init; } = null!;

        protected bool ShowModal = false;
        protected Product SelectedProduct { get; set; } = new Product();

        protected List<StoreBlazor.Client.Models.Product>? Products { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            Products = await ProductsHttpClient.GetAllProducts();

            await base.OnInitializedAsync();
        }

        protected void ModalShow(Product product)
        {
            ShowModal = true;
            SelectedProduct = product;
        }

        protected void ModalCancel()
        {
            ShowModal = false;
            SelectedProduct = new Product();
        }
    }
}
using StoreBlazor.Client.HttpClients;
using StoreBlazor.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace StoreBlazor.Client.Pages
{
    public class CreateCardBase : ComponentBase
    {
        [Inject] ProductsHttpClient ProductsHttpClient { get; init; } = null!;
        [Inject] NavigationManager NavigationManager { get; init; }

        
        protected StoreBlazor.Client.Models.Product Product { get; set; } = new StoreBlazor.Client.Models.Product();


        protected override async Task OnInitializedAsync()
        {


            await base.OnInitializedAsync();
        }
        protected void ModalShow(Product product)
        {

        }

        protected async void CreateOnClick()
        {
            await ProductsHttpClient.CreateCard(Product);
            NavigationManager.NavigateTo("/");
        }
    }
}
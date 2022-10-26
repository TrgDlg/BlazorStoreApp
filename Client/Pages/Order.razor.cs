using StoreBlazor.Client.HttpClients;
using StoreBlazor.Client.Models;
using Microsoft.AspNetCore.Components;

namespace StoreBlazor.Client.Pages
{
    public class OrderBase : ComponentBase
    {
        [Inject] ProductsHttpClient ProductsHttpClient { get; init; } = null!;





        protected override async Task OnInitializedAsync()
        {


            await base.OnInitializedAsync();
        }

    }
}
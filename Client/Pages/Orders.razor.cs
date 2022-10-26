using StoreBlazor.Client.HttpClients;
using StoreBlazor.Client.Models;
using Microsoft.AspNetCore.Components;

namespace StoreBlazor.Client.Pages
{
    public class OrdersBase : ComponentBase
    {
        [Inject] OrdersHttpClient OrdersHttpClient { get; init; } = null!;

        protected List<Order>? Orders { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            //Orders = await OrdersHttpClient.GetAllOrders();
            //Products = productsDto.Items;
            await base.OnInitializedAsync();
        }        
    }
}
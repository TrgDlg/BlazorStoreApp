using StoreBlazor.Client.Models;
using StoreBlazor.Server.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppStore.Server.Controllers
{
    [ApiController]
    [Route("api/")]
    public class BlazorShopController : ControllerBase
    {
        private readonly ILogger<BlazorShopController> _logger;
        private readonly IBlazorShopRepository _blazorShopRepository;

        public BlazorShopController(ILogger<BlazorShopController> logger,
            IBlazorShopRepository blazorShopRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _blazorShopRepository = blazorShopRepository ??
                                            throw new ArgumentNullException(nameof(blazorShopRepository));
        }

        [HttpGet("homepage/getallproducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _blazorShopRepository.GetAllProducts();

            return Ok(products);
        }

        [HttpGet("card/{searchvalue}")]
        public async Task<IActionResult> GetProductByName([FromRoute] string searchvalue)
        {
            var product = await _blazorShopRepository.GetProductByName(searchvalue);

            return Ok(product);
        }

        [HttpGet("orders/getallorders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var products = await _blazorShopRepository.GetAllOrders();

            return Ok(products);
        }


        [HttpPost("createcard")]
        public async Task CreateCard([FromBody] Product product)
        {
            await _blazorShopRepository.CreateProduct(product);
        }
    }

}
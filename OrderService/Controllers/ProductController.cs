using Microsoft.AspNetCore.Mvc;
using OrderService.Interfaces;
using OrderService.Models.DTOs;
using OrderService.Services;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            return await _productsService.GetAllProductsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _productsService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task CreateProduct(ProductDto productDto)
        { 
            await _productsService.CreateProductAsync(productDto);
        }

        [HttpPut("{id}")]
        public async Task UpdateProduct(int id, ProductDto productDto)
        {
            await _productsService.UpdateProductAsync(id, productDto);
        }

        [HttpDelete("{id}")]
        public async Task DeleteProduct(int id)
        {
            await _productsService.DeleteProductAsync(id);
        }
    }
}
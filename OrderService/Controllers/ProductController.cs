using Microsoft.AspNetCore.Mvc;
using OrderService.Models.DTOs;
using OrderService.Services;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductsService _productsService;

        public ProductController(ProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productsService.GetAllProductsAsync();
            return Ok(products);
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
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto productDto)
        {
            var created = await _productsService.CreateProductAsync(productDto);
            return Ok(created);
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
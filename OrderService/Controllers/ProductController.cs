using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;
using OrderService.Models.DTOs;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ProductController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _appDbContext.Products.ToListAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null)
                return NotFound($"Product with id {id} not found.");

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public async Task<ActionResult<CreateProductDto>> CreateProduct(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);

            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();

            var createdDto = _mapper.Map<ProductDto>(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            var existingProduct = await _appDbContext.Products.FindAsync(id);
            if (existingProduct == null)
                return NotFound($"Product with id {id} not found.");

            _mapper.Map(productDto, existingProduct);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productToDelete = await _appDbContext.Products.FindAsync(id);
            if (productToDelete == null)
                return NotFound($"Product with id {id} not found.");

            _appDbContext.Products.Remove(productToDelete);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}

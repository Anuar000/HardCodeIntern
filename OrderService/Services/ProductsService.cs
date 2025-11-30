using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Exceptions;
using OrderService.Models;
using OrderService.Models.DTOs;

namespace OrderService.Services
{
    public class ProductsService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductsService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            
            if (product == null)
                throw new NotFoundException($"Product with id {id} was not found");
            
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateProductAsync(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
                throw new BusinessException("ID mismatch");

            var product = await _context.Products.FindAsync(id);
            
            if (product == null)
                throw new NotFoundException($"Product with id {id} was not found");

            _mapper.Map(productDto, product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            
            if (product == null)
                throw new NotFoundException($"Product with id {id} was not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}

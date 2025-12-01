using OrderService.Models.DTOs;

namespace OrderService.Interfaces;

public interface IProductsService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto> GetProductByIdAsync(int id);
    Task CreateProductAsync(ProductDto dto);
    Task UpdateProductAsync(int id, ProductDto dto);
    Task DeleteProductAsync(int id);
}
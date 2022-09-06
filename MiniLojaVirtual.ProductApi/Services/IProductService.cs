using MiniLojaVirtual.ProductApi.Dtos;

namespace MiniLojaVirtual.ProductApi.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts();
    Task<ProductDto> GetProductById(int id);
    Task AddProduct(ProductDto productDto);
    Task UpdateProduct(ProductDto productDto);
    Task RemoveProduct(int id);
}

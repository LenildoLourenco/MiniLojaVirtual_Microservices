using MiniLojaVirtual.ProductApi.Dtos;

namespace MiniLojaVirtual.ProductApi.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategories();
    Task<IEnumerable<CategoryDto>> GetCategoriesProducts();
    Task<CategoryDto> GetCategoryById(int id);
    Task AddCategory(CategoryDto categoryDto);
    Task UpdateCategory(CategoryDto categoryDto);
    Task RemoveCategory(int id);
}

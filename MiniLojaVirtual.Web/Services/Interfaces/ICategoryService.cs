using MiniLojaVirtual.Web.Models;

namespace MiniLojaVirtual.Web.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> GetAllCategories();
}

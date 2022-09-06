using Microsoft.EntityFrameworkCore;
using MiniLojaVirtual.ProductApi.Context;
using MiniLojaVirtual.ProductApi.Models;

namespace MiniLojaVirtual.ProductApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products.ToListAsync();
    }
    public async Task<Product> GetById(int id)
    {
        return await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
    }
    public async Task<Product> Create(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChangesAsync();
        return product;
    }
    public async Task<Product> Update(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        _context.SaveChangesAsync();
        return product;
    }
    public async Task<Product> Delete(int id)
    {
        var product = await GetById(id);
        _context.Products.Remove(product);
        _context.SaveChangesAsync();
        return product;
    }
}

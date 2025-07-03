using Domain.DTOs;
using Domain.Entites;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductService(AppDataContext context) : IProductService
{
    public async Task<IEnumerable<ProductDTO>> GetAllAsync()
    {
        return await context.Products.Select(p => new ProductDTO
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price
        }).ToListAsync();
    }
    public async Task<ProductDTO?> GetByIdAsync(int id)
    {
        var p = await context.Products.FindAsync(id);
        return p == null ? null : new ProductDTO
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price
        };
    }
    public async Task<ProductDTO> CreateAsync(CreateAndUpdateProductDTO dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price
        };
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };
    }
    public async Task<bool> UpdateAsync(int id, CreateAndUpdateProductDTO dto)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null) return false;
        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        await context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null) return false;
        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return true;
    }
}


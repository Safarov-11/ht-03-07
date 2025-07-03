using Domain.DTOs;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    public Task<IEnumerable<ProductDTO>> GetAllAsync();
    public Task<ProductDTO?> GetByIdAsync(int id);
    public Task<ProductDTO> CreateAsync(CreateAndUpdateProductDTO productDTO);
    public Task<bool> UpdateAsync(int id, CreateAndUpdateProductDTO productDTO);
    public Task<bool> DeleteAsync(int id);
}

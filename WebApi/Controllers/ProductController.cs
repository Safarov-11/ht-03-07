using Domain.DTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService service) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll() => Ok(await service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await service.GetByIdAsync(id);
        return product == null ? NotFound() : Ok(product);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateAndUpdateProductDTO dto)
    {
        var product = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateAndUpdateProductDTO dto)
    {
        var updated = await service.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}

namespace Domain.DTOs;

public class CreateAndUpdateProductDTO
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
}

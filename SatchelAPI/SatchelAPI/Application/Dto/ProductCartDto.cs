namespace SatchelAPI.Application.Dto;

public class ProductCartDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<string>? Images { get; set; }
}
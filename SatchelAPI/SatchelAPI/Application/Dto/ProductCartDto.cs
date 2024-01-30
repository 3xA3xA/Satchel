namespace SatchelAPI.Application.Dto;

public class ProductCartDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<string>? Images { get; set; }
    public string SizeName { get; set; }
    public int SizeTypeId { get; set; }
}
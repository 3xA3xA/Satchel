namespace SatchelAPI.Application.Dto;

public class ProductCartDto
{
    public ProductCartDto()
    {
        
    }
    public ProductCartDto(int productId, string name, decimal price, List<string>? images, string sizeName, int sizeTypeId)
    {
        ProductId = productId;
        Name = name;
        Price = price;
        Images = images;
        SizeName = sizeName;
        SizeTypeId = sizeTypeId;
    }
    
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<string>? Images { get; set; }
    public string? SizeName { get; set; }
    public int? SizeTypeId { get; set; }
}
using SatchelAPI.Application.Models;

namespace SatchelAPI.Application.Dto;

public class ProductDto
{
    public ProductDto(
        string name, 
        string description,
        int productTypeId,
        decimal price,
        int brandTypeId,
        int genderTypeId)
    {
        Name = name;
        Description = description;
        ProductTypeId = productTypeId;
        Price = price;
        BrandTypeId = brandTypeId;
        GenderTypeId = genderTypeId;
    }
    
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProductTypeId { get; set; }
    public decimal Price { get; set; }
    public int BrandTypeId { get; set; }
    public int GenderTypeId { get; set; }
}
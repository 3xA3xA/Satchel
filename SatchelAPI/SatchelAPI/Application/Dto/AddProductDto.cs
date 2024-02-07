namespace SatchelAPI.Application.Dto;

public class AddProductDto
{
    public AddProductDto(
        string name,
        string description,
        int productTypeId,
        decimal price,
        int brandTypeId,
        int genderTypeId,
        int userId,
        ICollection<int> sizeTypeIds)
    {
        Name = name;
        Description = description;
        ProductTypeId = productTypeId;
        Price = price;
        BrandTypeId = brandTypeId;
        GenderTypeId = genderTypeId;
        UserId = userId;
        SizeTypeIds = sizeTypeIds;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public int ProductTypeId { get; set; }
    public decimal Price { get; set; }
    public int BrandTypeId { get; set; }
    public int GenderTypeId { get; set; }
    public int UserId { get; set; }
    public ICollection<int> SizeTypeIds { get; set; }
}
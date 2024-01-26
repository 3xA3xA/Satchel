namespace SatchelAPI.Application.Dto;

public class GetProductTypeDto
{
    public GetProductTypeDto(int productTypeId, string name)
    {
        ProductTypeId = productTypeId;
        Name = name;
    }
    
    public int ProductTypeId { get; set; }
    public string Name { get; set; }
}
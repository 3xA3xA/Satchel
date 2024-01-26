namespace SatchelAPI.Application.Dto;

public class GetBrandTypeDto
{
    public GetBrandTypeDto(int brandTypeId, string name)
    {
        BrandTypeId = brandTypeId;
        Name = name;
    }
    
    public int BrandTypeId { get; set; }
    public string Name { get; set; }
}
namespace SatchelAPI.Application.Dto;

public class GetGenderTypeDto
{
    public GetGenderTypeDto(int genderTypeId, string name)
    {
        GenderTypeId = genderTypeId;
        Name = name;
    }
    
    public int GenderTypeId { get; set; }
    public string Name { get; set; }
}
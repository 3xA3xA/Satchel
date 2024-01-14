namespace SatchelAPI.Application.Dto;

public class GetViewUserDto
{
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? UserPhotoSrc { get; set; }
}
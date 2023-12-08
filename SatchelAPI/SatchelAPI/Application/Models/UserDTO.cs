namespace SatchelAPI.Application.Models
{
    public class UserDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int UserTypeId { get; set; }
    }
}

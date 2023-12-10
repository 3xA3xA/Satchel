using Satchel.Application.Models;

namespace SatchelAPI.Application.Models
{
    public class UserDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string UserTypeName { get; set; }
    }
}

using Microsoft.Build.Framework;
using Satchel.Application.Models;

namespace SatchelAPI.Application.Dto
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? UserTypeName { get; set; }
        public int? UserTypeId { get; set; }
    }
}

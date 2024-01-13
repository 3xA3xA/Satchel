using Satchel.Application.Models;
using SatchelAPI.Application.Dto;

namespace SatchelAPI.Interfaces.ServicesInterfaces
{
    public interface IUserService
    {
        public Task<GetUserDTO> LoginUser(string login, string password);
        public string HashPassword(string password);
        public Task<GetUserDTO> AddNewUser(UserDTO userData);
        public bool UserExists(string email);
    }
}

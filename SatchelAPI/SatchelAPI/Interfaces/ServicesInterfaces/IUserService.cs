﻿using Satchel.Application.Models;
using SatchelAPI.Application.Dto;

namespace SatchelAPI.Interfaces.ServicesInterfaces
{
    public interface IUserService
    {
        public Task<GetUserDTO> LoginUser(string login, string password);
        public string HashPassword(string password);
        public Task<GetUserDTO> AddNewUser(UserDTO userData);
        public bool UserExists(string email);
        public bool EmailExist(string email, int userId);
        public Task<GetViewUserDto> GetViewUserData(int userId);
        public Task UpdateProfileInfoUser(int userId, GetViewUserDto viewUserDto);
    }
}

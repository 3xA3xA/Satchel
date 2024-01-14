using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Satchel.Application.Models;
using Satchel.Infrastructure;
using SatchelAPI.Application.Dto;
using SatchelAPI.Interfaces.ServicesInterfaces;
using static NuGet.Packaging.PackagingConstants;
using IUserService = SatchelAPI.Interfaces.ServicesInterfaces.IUserService;

namespace SatchelAPI.Services
{
    public class UserService : IUserService
    {
        private readonly SatchelDbContext _context;
        private readonly IMapper _mapper;

        public UserService(SatchelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public async Task<GetUserDTO?> LoginUser(string login, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(e => e.Email == login && e.Password == HashPassword(password));

            return _mapper.Map<GetUserDTO>(user);
        }

        public async Task<GetUserDTO?> AddNewUser(UserDTO userData)
        {
            UserType? userType = _context.UserTypes.FirstOrDefault(ut => ut.Name == userData.UserTypeName);

            userData.UserTypeId = userType!.UserTypeId;

            var newUser = _mapper.Map<User>(userData);
            newUser.Password = HashPassword(newUser.Password);

            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetUserDTO>(newUser);
        }

        public bool UserExists(string email)
        {
            return _context.User.Any(e => e.Email == email);
        }

        private async Task<User> GetUser(int userId)
        {
            return await _context.User
                .Include(_ => _.UserType)
                .FirstOrDefaultAsync(_ => _.UserId == userId);
        }

        public async Task<GetViewUserDto> GetViewUserData(int userId)
        {
            var user = await GetUser(userId);
            var getViewUserDto = _mapper.Map<GetViewUserDto>(user);

            return getViewUserDto;
        }

        private void SetNewValuesToUser(User user, GetViewUserDto viewUserDto)
        {
            user.FirstName = viewUserDto.FirstName;
            user.LastName = viewUserDto.LastName;
            user.MiddleName = viewUserDto.MiddleName;
            user.Email = viewUserDto.Email;
            user.Image = viewUserDto.UserPhotoSrc;
            user.Birthday = viewUserDto.DateOfBirth;
        }

        public async Task UpdateProfileInfoUser(int userId, GetViewUserDto viewUserDto)
        {
            var user = await GetUser(userId);
            SetNewValuesToUser(user, viewUserDto);

            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}

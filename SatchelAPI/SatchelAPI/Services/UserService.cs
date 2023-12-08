using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Satchel.Application.Models;
using Satchel.Infrastructure;
using SatchelAPI.Application.Models;
using static NuGet.Packaging.PackagingConstants;

namespace SatchelAPI.Services
{
    public class UserService
    {
        public static string HashPassword(string password)
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

        //private string CheckValidData(UserDTO data, UserType userType)
        //{
        //    if (data == null)
        //    {
        //        return "Неверные данные пользователя";
        //    }

        //    if (userType == null)
        //    {
        //        return "Тип пользователя не существует";
        //    }
        //    if (UserExists(data.Email))
        //    {
        //        return BadRequest("Пользователь с таким email уже существует");
        //    }

        //    return Ok();
        //}
    }
}

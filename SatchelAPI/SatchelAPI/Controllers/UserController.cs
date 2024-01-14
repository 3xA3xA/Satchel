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
using SatchelAPI.Application.Dto;
using SatchelAPI.Interfaces.ServicesInterfaces;
using SatchelAPI.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static NuGet.Packaging.PackagingConstants;

namespace SatchelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginUser([FromBody] UserDTO userData)
        {
            if (_service.UserExists(userData.Email))
            {
                var user = await _service.LoginUser(userData.Email, userData.Password);

                if (user != null)
                    return Ok(user);
                else
                    return Unauthorized("Пароль и email не совпадают!");
            }
            return Unauthorized("Неверный email");
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userData)
        {
            if (_service.UserExists(userData.Email))
            {
                return Unauthorized("Пользователь с таким email уже существует");
            }

            var user = await _service.AddNewUser(userData);

            if (user == null) 
                return Unauthorized("Ошибка создания аккаунта!");

            return Ok(user);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetViewUserDate(int userId)
        {
            try
            {
                var getViewUser = await _service.GetViewUserDate(userId);
                return Ok(getViewUser);
            }
            catch (Exception  e)
            {
                return BadRequest(e);
            }
        }
    }
}

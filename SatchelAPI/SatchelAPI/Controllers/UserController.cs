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
using SatchelAPI.Services;
using static NuGet.Packaging.PackagingConstants;

namespace SatchelAPI.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly SatchelDbContext _context;

        public UserController(SatchelDbContext context)
        {
            _context = context;
        }

        private IActionResult CheckValidData(UserDTO data) // вынести бы!
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Неверные данные пользователя");
            }

            int userTypeId = data.UserTypeId;
            if (data.UserTypeId == 0)
            {
                Console.Out.WriteLine(data.Email);
                userTypeId = _context.User.FirstOrDefault(e => e.Email == data.Email).UserTypeId;
            }

            UserType? userType = _context.UserTypes.Find(userTypeId);           

            if (userType == null)
            {
                return BadRequest("Тип пользователя не существует");
            }
            if (data.UserTypeId != 0 && UserExists(data.Email)) // дурацкая проверка
            {
                return BadRequest("Пользователь с таким email уже существует");
            }

            return Ok();
        }

        [HttpPost, ActionName("LoginUser")]
        [Route("api/LoginUser")]
        //[ValidateAntiForgeryToken] - это важная штука, после настроим
        public async Task<IActionResult> LoginUser([FromBody] UserDTO userData)
        {
            var validationAnswer = CheckValidData(userData);
            await Console.Out.WriteLineAsync("После валидации");
            if (validationAnswer is OkResult)
            {
                await Console.Out.WriteLineAsync("WROK");
                return Ok(userData);
            }

            return validationAnswer;
        }


        [HttpPost, ActionName("CreateUser")]
        [Route("api/CreateUser")]
        //[ValidateAntiForgeryToken] - это важная штука, после настроим
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userData)
        {
            UserType? userType = _context.UserTypes.Find(userData.UserTypeId);

            var validationAnswer = CheckValidData(userData);

            if ( validationAnswer is OkResult)
            {
                User newUser = new User
                {
                    Email = userData.Email,
                    Password = userData.Password,
                    UserTypeId = userData.UserTypeId,
                    UserType = userType,
                    Feedbacks = new List<Feedback>(),
                    Orders = new List<Order>(),
                    Favourites = new List<Favourites>(),
                    ShoppingCarts = new List<ShoppingCart>(),
                };

                newUser.Password = UserService.HashPassword(newUser.Password);
                _context.Add(newUser);
                await _context.SaveChangesAsync();
                return Ok(userData);
            }

            return validationAnswer;
        }

        private bool UserExists(string email)
        {
            return _context.User.Any(e => e.Email == email);
        }        
    }
}

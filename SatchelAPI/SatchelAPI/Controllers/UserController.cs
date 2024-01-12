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
using static System.Runtime.InteropServices.JavaScript.JSType;
using static NuGet.Packaging.PackagingConstants;

namespace SatchelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly SatchelDbContext _context;

        public UserController(SatchelDbContext context)
        {
            _context = context;
        }

        [HttpPost("[action]")]
        //[ValidateAntiForgeryToken] - это важная штука, после настроим
        public IActionResult LoginUser([FromBody] UserDTO userData)
        {
            if (ModelState.IsValid && UserExists(userData.Email))
            {
                return Ok(userData); // тут нужно будет найти пользователя в бд
            }

            return BadRequest("Пользователь с таким email уже существует");           
        }


        [HttpPost("[action]")]
        //[ValidateAntiForgeryToken] - это важная штука, после настроим
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Неверные данные пользователя");
            }

            if (UserExists(userData.Email))
            {
                return BadRequest("Пользователь с таким email уже существует");
            }

            UserType userType = _context.UserTypes.FirstOrDefault(ut => ut.Name == userData.UserTypeName);

            User newUser = new User
            {
                Email = userData.Email,
                Password = userData.Password,
                UserTypeId = userType.UserTypeId,
                UserType = userType,
                Feedbacks = new List<Feedback>(),
                Orders = new List<Order>(),
                Favourites = new List<Favourites>(),
                ShoppingCarts = new List<ShoppingCart>(),
            };

            //newUser.Password = UserService.HashPassword(newUser.Password);
            _context.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok(userData);
        }

        private bool UserExists(string email)
        {
            return _context.User.Any(e => e.Email == email);
        }        
    }
}

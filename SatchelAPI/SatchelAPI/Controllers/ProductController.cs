using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Satchel.Application.Models;
using Satchel.Infrastructure;
using SatchelAPI.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SatchelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly SatchelDbContext _context;

        public ProductController(SatchelDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Object>>> getAllClothes()
        {
            IQueryable<Product> query = _context.Products;

            //ProductService.getAllClothes();

            // Дописать поля, которые хотите получать.
            var productsWithImages = await query
           .Select(p => new
           {
               Id = p.ProductId,
               Name = p.Name,
               Price = p.Price,
               Images = p.ProductImages.Select(img => img.ImagePath).ToList(), //хз  // создать модель ProductDTO либо интерфейс
           })
           .ToListAsync();

            return productsWithImages;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Object> GetProductById(int id)
        {
            IQueryable<Product> query = _context.Products.Where(p => p.ProductId == id);

            
            var product = query
               .Select(p => new
               {
                   Id = p.ProductId,
                   Name = p.Name,
                   Description = p.Description,
                   ProductTypeId = p.ProductTypeId,
                   Price = p.Price,
                   BrandTypeId = p.BrandTypeId,
                   GenderTypeId = p.GenderTypeId,
                   Sizes = p.ProductType.SizeTypeToProductTypes.Select(sizes => sizes.SizeType.Name).ToList(),
                   Images = p.ProductImages.Select(img => img.ImagePath).ToList(),
               }).FirstOrDefault();

            return product;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Satchel.Application.Models;
using Satchel.Infrastructure;
using SatchelAPI.Application.Models;
using SatchelAPI.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SatchelAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly SatchelDbContext _context;
        private readonly IMapper _mapper;

        public ProductController(SatchelDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("{productType}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllClothes(string productType)
        {
            await Console.Out.WriteLineAsync(productType);
            IQueryable<Product> query = _context.Products.Include(p => p.ProductType);

            await Console.Out.WriteLineAsync(productType + '1');

            // Дописать поля, которые хотим получать.
            var productsWithImages = await query
            .Where(p => p.ProductType.Name == productType)
            .Select(p => new ProductDTO
            {
                Id = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Images = p.ProductImages.Select(img => img.ImagePath).ToList(),
            })
            .ToListAsync();

            return productsWithImages;
        }

        [HttpGet("Product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductDTO> GetProductById(int id)
        {
            IQueryable<Product> query = _context.Products.Where(p => p.ProductId == id);

            
            var product = query
               .Select(p => new ProductDTO
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

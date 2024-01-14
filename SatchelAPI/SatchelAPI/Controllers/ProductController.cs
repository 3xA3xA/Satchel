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
using SatchelAPI.Application.Dto;
using SatchelAPI.Application.Models;
using SatchelAPI.Interfaces.ServicesInterfaces;
using SatchelAPI.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SatchelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("[action]/{productType}")]
        public async Task<IActionResult> GetAllProducts(
            string productType, 
            int? filterByMinPrice, 
            int? filterByMaxPrice, 
            int? filterByGender,
            string? filterByName,
            bool isFilterByDecreasePrice,
            bool isFilterByIncreasePrice)
        {
            try
            {
                var products = await _service.GetAllProducts(productType, filterByMinPrice,
                    filterByMaxPrice, filterByGender, filterByName, isFilterByDecreasePrice, isFilterByIncreasePrice);
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _service.GetProduct(id);
                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        public class ProductData
        {
            public ProductDto ProductDto { get; set; }
            public ICollection<ProductImageDto> AddProductImagesDto { get; set; }
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> AddProduct([FromBody] ProductData addProductData)
        {
            try
            {
                await _service.AddProduct(addProductData.ProductDto, addProductData.AddProductImagesDto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
        {
            try
            {
                await _service.DeleteProduct(productId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductData productData)
        {
            try
            {
                await _service.UpdateProduct(productId, productData.ProductDto, productData.AddProductImagesDto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}

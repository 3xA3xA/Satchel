using Microsoft.AspNetCore.Mvc;
using SatchelAPI.Application.Dto;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingCartController : Controller
{
    private readonly IShoppingCartService _service;
    
    public ShoppingCartController(IShoppingCartService service)
    {
        _service = service;
    }
    
    [HttpGet("[action]")]
    [ProducesResponseType(typeof(ProductCartDto), 200)]
    public async Task<IActionResult> GetShoppingCart(int userId)
    {
        try
        {
            var response = await _service.GetShoppingCart(userId);
            return Ok(response);
        }
        catch (ArgumentNullException)
        {
            return NoContent();
        }
        catch (Exception  e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddProductToShoppingCart(int productId, int userId, string sizeTypeName)
    {
        try
        {
            await _service.AddProductToShoppingCart(productId, userId, sizeTypeName);
            return Ok();
        }
        catch (Exception  e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteProductFromShoppingCart(int productId, int userId, int sizeTypeId)
    {
        try
        {
            await _service.DeleteProductFromShoppingCart(productId, userId, sizeTypeId);
            return Ok();
        }
        catch (Exception  e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteAllProductFromShoppingCart(int userId)
    {
        try
        {
            await _service.DeleteAllProductFromShoppingCart(userId);
            return Ok();
        }
        catch (Exception  e)
        {
            return BadRequest(e);
        }
    }
}
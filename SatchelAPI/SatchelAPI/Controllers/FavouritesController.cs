using Microsoft.AspNetCore.Mvc;
using SatchelAPI.Application.Dto;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavouritesController : Controller
{
    private readonly IFavouritesService _service;
    
    public FavouritesController(IFavouritesService service)
    {
        _service = service;
    }
    
    [HttpGet("[action]")]
    [ProducesResponseType(typeof(ProductCartDto), 200)]
    public async Task<IActionResult> GetFavourites(int userId)
    {
        try
        {
            var response = await _service.GetFavourites(userId);
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
    public async Task<IActionResult> AddProductToFavourites(int productId, int userId)
    {
        try
        {
            await _service.AddProductToFavourites(productId, userId);
            return Ok();
        }
        catch (Exception  e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteProductFromFavourites(int productId, int userId)
    {
        try
        {
            await _service.DeleteProductFromFavourites(productId, userId);
            return Ok();
        }
        catch (Exception  e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteAllProductFromFavourites(int userId)
    {
        try
        {
            await _service.DeleteAllProductFromFavourites(userId);
            return Ok();
        }
        catch (Exception  e)
        {
            return BadRequest(e);
        }
    }
}
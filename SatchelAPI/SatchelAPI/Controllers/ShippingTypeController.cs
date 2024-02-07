using Microsoft.AspNetCore.Mvc;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShippingTypeController : Controller
{
    private readonly IShippingTypeService _service;
    
    public ShippingTypeController(IShippingTypeService service)
    {
        _service = service;
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetShippingTypes()
    {
        try
        {
            var response = await _service.GetShippingTypes();
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}
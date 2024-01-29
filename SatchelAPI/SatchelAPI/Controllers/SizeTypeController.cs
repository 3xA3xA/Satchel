using Microsoft.AspNetCore.Mvc;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SizeTypeController  : Controller
{
    private readonly ISizeTypeService _service;
    
    public SizeTypeController(ISizeTypeService service)
    {
        _service = service;
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetSizeTypesByProductType(string productTypeName)
    {
        try
        {
            var response = await _service.GetSizeTypesByProductType(productTypeName);
            return Ok(response);
        }
        catch (Exception  e)
        {
            return BadRequest(e);
        }
    }
}
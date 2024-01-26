using Microsoft.AspNetCore.Mvc;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandTypeController : Controller
{
    private readonly IBrandService _service;
    
    public BrandTypeController(IBrandService service)
    {
        _service = service;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GetBrandTypes()
    {
        try
        {
            var response = await _service.GetBrandTypes();
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}
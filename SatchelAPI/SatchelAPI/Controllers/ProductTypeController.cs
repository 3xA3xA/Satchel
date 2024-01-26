using Microsoft.AspNetCore.Mvc;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductTypeController : Controller
{
    private readonly IProductTypeService _service;
    
    public ProductTypeController(IProductTypeService service)
    {
        _service = service;
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetProductTypes()
    {
        try
        {
            var response = await _service.GetProductTypes();
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}
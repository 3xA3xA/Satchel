using Microsoft.AspNetCore.Mvc;
using SatchelAPI.Application.Dto;
using SatchelAPI.Interfaces;

namespace SatchelAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenderTypeController : Controller
{
    private readonly IGenderTypeService _service;

    public GenderTypeController(IGenderTypeService service)
    {
        _service = service;
    }
    
    [HttpGet("[action]")]
    [ProducesResponseType(typeof(IEnumerable<GetGenderTypeDto>), 200)]
    public async Task<IActionResult> GetGenderTypes()
    {
        try
        {
            var getViewUser = await _service.GetGenderTypes();
            return Ok(getViewUser);
        }
        catch (Exception  e)
        {
            return BadRequest(e);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentTypeController : Controller
{
    private readonly IPaymentTypeService _service;
    
    public PaymentTypeController(IPaymentTypeService service)
    {
        _service = service;
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetPaymentTypes()
    {
        try
        {
            var response = await _service.GetPaymentTypes();
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}
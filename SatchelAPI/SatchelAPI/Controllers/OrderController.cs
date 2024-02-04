using Microsoft.AspNetCore.Mvc;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : Controller
{
    private readonly IOrderService _service;
    
    public OrderController(IOrderService service)
    {
        _service = service;
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> FormingOrders(int userId, int paymentTypeId, int shippingTypeId)
    {
        try
        {
            await _service.FormingOrders(userId, paymentTypeId, shippingTypeId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}
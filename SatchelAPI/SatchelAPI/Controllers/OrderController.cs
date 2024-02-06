using Microsoft.AspNetCore.Mvc;
using SatchelAPI.Application.Dto;
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
    
    [HttpPost("[action]")]
    public async Task<IActionResult> FormingOrders(FormingOrderDto formingOrderDto)
    {
        try
        {
            await _service.FormingOrders(formingOrderDto.UserId, formingOrderDto.UserId, formingOrderDto.ShippingTypeId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}
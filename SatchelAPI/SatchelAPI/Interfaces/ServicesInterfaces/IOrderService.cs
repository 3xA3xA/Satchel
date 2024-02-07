using Satchel.Application.Models;
using SatchelAPI.Application.Dto;

namespace SatchelAPI.Interfaces.ServicesInterfaces;

public interface IOrderService
{
    public Task FormingOrders(int userId, int paymentTypeId, int shippingTypeId);
    public Task<IEnumerable<GetOrderDto>> GetOrders(int userId);
}
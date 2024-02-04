using Satchel.Application.Models;

namespace SatchelAPI.Interfaces.ServicesInterfaces;

public interface IOrderService
{
    public Task FormingOrders(int userId, int paymentTypeId, int shippingTypeId);
}
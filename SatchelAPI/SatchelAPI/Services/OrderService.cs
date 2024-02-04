using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Satchel.Application.Models;
using Satchel.Infrastructure;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Services;

public class OrderService : IOrderService
{
    private readonly SatchelDbContext _context;
    private readonly IMapper _mapper;
    
    public OrderService(SatchelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task FormingOrders(int userId, int paymentTypeId, int shippingTypeId)
    {
        var productIds = await _context.ShoppingCarts
            .Where(_ => _.UserId == userId)
            .Select(_ => _.ProductId)
            .ToListAsync();

        var orders = new List<Order>();
        foreach (var productId in productIds)
        {
            orders.Add(new Order(productId, userId, 1, paymentTypeId, shippingTypeId));
        }

        await _context.AddRangeAsync(orders);
        await _context.SaveChangesAsync();
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Satchel.Application.Models;
using Satchel.Infrastructure;
using SatchelAPI.Application.Dto;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Services;

public class OrderService : IOrderService
{
    private readonly SatchelDbContext _context;
    private readonly IMapper _mapper;
    private readonly Random _random = new Random();
    
    public OrderService(SatchelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private async Task<List<Order>> GetOrdersByUserId(int userId)
    {
        return await _context.Orders
            .Include(_ => _.ShippingType)
            .Include(_ => _.OrderStatusType)
            .Include(_ => _.Product)
            .ThenInclude(_ => _.ProductImages)
            .Where(_ => _.UserId == userId)
            .ToListAsync();
    }

    private async Task<int> GetReceiptCode(int userId)
    {
        var orders = await GetOrdersByUserId(userId);
        
        if (orders.Count != 0)
        {
            return orders.First().ReceiptCode;
        }
        
        return _random.Next(100, 1000);
    }
    
    public async Task FormingOrders(int userId, int paymentTypeId, int shippingTypeId)
    {
        var productIds = await _context.ShoppingCarts
            .Where(_ => _.UserId == userId)
            .Select(_ => _.ProductId)
            .ToListAsync();

        int receiptCode = await GetReceiptCode(userId);
        
        var orders = new List<Order>();
        foreach (var productId in productIds)
        {
            orders.Add(new Order(productId, userId, 1, paymentTypeId, shippingTypeId, receiptCode));
        }

        await _context.AddRangeAsync(orders);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<GetOrderDto>> GetOrders(int userId)
    {
        var orders = await GetOrdersByUserId(userId);

        var products = orders.Select(_ => _.Product).ToList();
        
        var getOrdersDtos = _mapper.Map<List<GetOrderDto>>(orders);
        var productCards = _mapper.Map<List<ProductCartDto>>(products);

        for (int i = 0; i < getOrdersDtos.Count; i++)
        {
            getOrdersDtos[i].ProductCartDto = productCards[i];
        }

        return getOrdersDtos;
    }
}
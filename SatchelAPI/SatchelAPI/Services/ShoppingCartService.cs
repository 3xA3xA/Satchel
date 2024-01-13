using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Satchel.Application.Models;
using Satchel.Infrastructure;
using SatchelAPI.Application.Dto;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly SatchelDbContext _context;
    private readonly IMapper _mapper;
    
    public ShoppingCartService(SatchelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private async Task<List<Product>> GetProductsFromShoppingCart(int userId)
    {
        return await _context.ShoppingCarts
            .Include(_ => _.Product)
            .ThenInclude(_ => _.ProductImages)
            .Where(_ => _.UserId == userId)
            .Select(_ => _.Product)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductCartDto>> GetShoppingCart(int userId)
    {
        var products = await GetProductsFromShoppingCart(userId);
        var response = _mapper.Map<IEnumerable<ProductCartDto>>(products);

        return response;
    }

    public async Task AddProductToShoppingCart(int productId, int userId)
    {
        var newShoppingCart = new ShoppingCart(productId, userId);
        await _context.ShoppingCarts.AddAsync(newShoppingCart);
        await _context.SaveChangesAsync();
    }

    private async Task<ShoppingCart> GetShoppingCart(int productId, int userId)
    {
        return await _context.ShoppingCarts
            .FirstOrDefaultAsync(_ => _.UserId == userId && _.ProductId == productId);
    }

    public async Task DeleteProductFromShoppingCart(int productId, int userId)
    {
        var deleteShoppingCart = await GetShoppingCart(productId, userId);
        _context.ShoppingCarts.Remove(deleteShoppingCart);
        await _context.SaveChangesAsync();
    }

    private async Task<IEnumerable<ShoppingCart>> GetShoppingCarts(int userId)
    {
        return await _context.ShoppingCarts
            .Where(_ => _.UserId == userId)
            .ToListAsync();
    }

    public async Task DeleteAllProductFromShoppingCart(int userId)
    {
        var deleteShoppingCarts = await GetShoppingCarts(userId);
        _context.ShoppingCarts.RemoveRange(deleteShoppingCarts);
        await _context.SaveChangesAsync();
    }
}
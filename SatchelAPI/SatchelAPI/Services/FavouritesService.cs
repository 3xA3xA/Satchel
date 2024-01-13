using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Satchel.Application.Models;
using Satchel.Infrastructure;
using SatchelAPI.Application.Dto;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Services;

public class FavouritesService : IFavouritesService
{
    private readonly SatchelDbContext _context;
    private readonly IMapper _mapper;
    
    public FavouritesService(SatchelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private async Task<List<Product>> GetProductsFromFavourites(int userId)
    {
        return await _context.Favourites
            .Include(_ => _.Product)
            .ThenInclude(_ => _.ProductImages)
            .Where(_ => _.UserId == userId)
            .Select(_ => _.Product)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductCartDto>> GetFavourites(int userId)
    {
        var products = await GetProductsFromFavourites(userId);
        var response = _mapper.Map<IEnumerable<ProductCartDto>>(products);

        return response;
    }

    public async Task AddProductToFavourites(int productId, int userId)
    {
        var newFavourite = new Favourites(productId, userId);
        await _context.Favourites.AddAsync(newFavourite);
        await _context.SaveChangesAsync();
    }

    private async Task<Favourites> GetFavourite(int productId, int userId)
    {
        return await _context.Favourites
            .FirstOrDefaultAsync(_ => _.UserId == userId && _.ProductId == productId);
    }

    public async Task DeleteProductFromFavourites(int productId, int userId)
    {
        var deleteFavourite = await GetFavourite(productId, userId);
        _context.Favourites.Remove(deleteFavourite);
        await _context.SaveChangesAsync();
    }

    private async Task<IEnumerable<Favourites>> GetFavouritesFromUserId(int userId)
    {
        return await _context.Favourites
            .Where(_ => _.UserId == userId)
            .ToListAsync();
    }

    public async Task DeleteAllProductFromFavourites(int userId)
    {
        var deleteFavourites = await GetFavouritesFromUserId(userId);
        _context.Favourites.RemoveRange(deleteFavourites);
        await _context.SaveChangesAsync();
    }
}
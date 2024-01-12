using SatchelAPI.Application.Dto;

namespace SatchelAPI.Interfaces.ServicesInterfaces;

public interface IFavouritesService
{
    public Task<IEnumerable<ProductCartDto>> GetFavourites(int userId);
    public Task AddProductToFavourites(int productId, int userId);
    public Task DeleteProductFromFavourites(int productId, int userId);
    public Task DeleteAllProductFromFavourites(int userId);
}
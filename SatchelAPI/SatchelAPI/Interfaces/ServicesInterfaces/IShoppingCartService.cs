using SatchelAPI.Application.Dto;

namespace SatchelAPI.Interfaces.ServicesInterfaces;

public interface IShoppingCartService
{
    public Task<IEnumerable<ProductCartDto>> GetShoppingCart(int userId);
    public Task AddProductToShoppingCart(int productId, int userId, int sizeTypeId);
    public Task DeleteProductFromShoppingCart(int productId, int userId);
    public Task DeleteAllProductFromShoppingCart(int userId);
}
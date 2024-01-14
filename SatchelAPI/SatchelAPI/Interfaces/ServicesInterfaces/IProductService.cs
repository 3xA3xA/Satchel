using SatchelAPI.Application.Dto;

namespace SatchelAPI.Interfaces.ServicesInterfaces;

public interface IProductService
{
    public Task AddProduct(ProductDto productDto, ICollection<ProductImageDto> addProductImagesDto);
    public Task DeleteProduct(int productId);
    public Task UpdateProduct(int productId, ProductDto productDto, ICollection<ProductImageDto> productImageDto);
    public Task<IEnumerable<ProductCartDto>> GetAllProducts(string productType, int? filterByMinPrice, int? filterByMaxPrice,
        int? filterByGender, string? filterByName, bool isFilterByDecreasePrice, bool isFilterByIncreasePrice);
    public Task<GetProductDto> GetProduct(int productId);
    public Task<IEnumerable<ProductCartDto>> GetSellerProducts(int userId);
}
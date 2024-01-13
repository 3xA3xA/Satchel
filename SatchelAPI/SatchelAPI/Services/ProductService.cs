using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Satchel.Application.Models;
using Satchel.Infrastructure;
using SatchelAPI.Application.Dto;
using SatchelAPI.Application.Models;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly SatchelDbContext _context;
        private readonly IMapper _mapper;
    
        public ProductService(SatchelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private void AddProductIdToProductImagesDto(ICollection<ProductImageDto> addProductImagesDto, int productId)
        {
            foreach (var addProductImageDto in addProductImagesDto)
            {
                addProductImageDto.ProductId = productId;
            }
        }
        
        public async Task AddProduct(ProductDto productDto, ICollection<ProductImageDto> addProductImagesDto)
        {
            var newProduct = _mapper.Map<Product>(productDto);
            AddProductIdToProductImagesDto(addProductImagesDto, newProduct.ProductId);
            var newProductImage = _mapper.Map<ICollection<ProductImages>>(addProductImagesDto);
            newProduct.ProductImages = newProductImage;

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }

        private async Task<Product> GetProduct(int productId)
        {
            return await _context.Products
                .FirstOrDefaultAsync(_ => _.ProductId == productId);
        }

        private async Task<ICollection<ProductImages>> GetProductImagesByProductId(int productId)
        {
            return await _context.ProductImages
                .Where(_ => _.ProductId == productId)
                .ToListAsync();
        }

        private async Task<IEnumerable<Feedback>> GetFeedbacksByProductId(int productId)
        {
            return await _context.Feedbacks
                .Where(_ => _.ProductId == productId)
                .ToListAsync();
        }

        private async Task<IEnumerable<ShoppingCart>> GetShoppingCartByProductId(int productId)
        {
            return await _context.ShoppingCarts
                .Where(_ => _.ProductId == productId)
                .ToListAsync();
        }

        private async Task<IEnumerable<Favourites>> GetFavouritesByProductId(int productId)
        {
            return await _context.Favourites
                .Where(_ => _.ProductId == productId)
                .ToListAsync();
        }

        private async Task<IEnumerable<Order>> GetOrdersByProductId(int productId)
        {
            return await _context.Orders
                .Where(_ => _.ProductId == productId)
                .ToListAsync();
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await GetProduct(productId);
            var productImages = await GetProductImagesByProductId(productId);
            var feedbacks = await GetFeedbacksByProductId(productId);
            var shoppingCarts = await GetShoppingCartByProductId(productId);
            var favourites = await GetFavouritesByProductId(productId);
            var orders = await GetOrdersByProductId(productId);
                
            _context.ProductImages.RemoveRange(productImages);
            _context.Feedbacks.RemoveRange(feedbacks);
            _context.ShoppingCarts.RemoveRange(shoppingCarts);
            _context.Favourites.RemoveRange(favourites);
            _context.Orders.RemoveRange(orders);
            _context.Products.Remove(product);
            
            await _context.SaveChangesAsync();
        }

        private async Task UpdateProductImages(int productId, ICollection<ProductImageDto> productImageDto)
        {
            var productImages = await GetProductImagesByProductId(productId);
            AddProductIdToProductImagesDto(productImageDto, productId);
            var newProductImages = _mapper.Map<ICollection<ProductImages>>(productImageDto);
            
            _context.ProductImages.RemoveRange(productImages);
            await _context.ProductImages.AddRangeAsync(newProductImages);
        }

        private void SetProductNewValues(Product product, ProductDto productDto)
        {
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.ProductTypeId = productDto.ProductTypeId;
            product.BrandTypeId = productDto.BrandTypeId;
            product.GenderTypeId = productDto.GenderTypeId;
        }

        public async Task UpdateProduct(int productId, ProductDto productDto, ICollection<ProductImageDto> productImageDto)
        {
            var updateProduct = await GetProduct(productId);
            await UpdateProductImages(productId, productImageDto);
            SetProductNewValues(updateProduct, productDto);

            await _context.Products.AddAsync(updateProduct);
            await _context.SaveChangesAsync();
        }
    }
}

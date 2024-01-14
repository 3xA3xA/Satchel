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

        private async Task<Product> GetProductById(int productId)
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
            var product = await GetProductById(productId);
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
            var updateProduct = await GetProductById(productId);
            await UpdateProductImages(productId, productImageDto);
            SetProductNewValues(updateProduct, productDto);

            _context.Products.Update(updateProduct);
            await _context.SaveChangesAsync();
        }

        private List<Product> FiltrationProductsByMinPrice(List<Product> products, int? filterByMinPrice)
        {
            if (filterByMinPrice != null)
            {
                return products.Where(_ => _.Price >= filterByMinPrice).ToList();
            }

            return products;
        }
        
        private List<Product> FiltrationProductsByMaxPrice(List<Product> products, int? filterByMaxPrice)
        {
            if (filterByMaxPrice != null)
            {
                return products.Where(_ => _.Price <= filterByMaxPrice).ToList();
            }

            return products;
        }
        
        private List<Product> FiltrationProductsByGender(List<Product> products, int? filterByGender)
        {
            if (filterByGender != null)
            {
                return products.Where(_ => _.GenderTypeId == filterByGender).ToList();
            }

            return products;
        }
        
        private List<Product> FiltrationProductsByName(List<Product> products, string? filterByName)
        {
            if (filterByName != null)
            {
                return products.Where(_ => _.Name.Contains(filterByName)).ToList();
            }

            return products;
        }
        
        private List<Product> FiltrationProductsByDecreasePrice(List<Product> products, bool isFilterByDecreasePrice)
        {
            if (isFilterByDecreasePrice)
            {
                return products.OrderBy(_ => _.Price).ToList();
            }

            return products;
        }
        
        private List<Product> FiltrationProductsByIncreasePrice(List<Product> products, bool isFilterByIncreasePrice)
        {
            if (isFilterByIncreasePrice)
            {
                return products.OrderByDescending(_ => _.Price).ToList();
            }

            return products;
        }

        private List<Product> FiltrationProducts(
            List<Product> products,
            int? filterByMinPrice,
            int? filterByMaxPrice,
            int? filterByGender,
            string? filterByName,
            bool isFilterByDecreasePrice,
            bool isFilterByIncreasePrice)
        {
            products = FiltrationProductsByMinPrice(products, filterByMinPrice);
            products = FiltrationProductsByMaxPrice(products, filterByMaxPrice);
            products = FiltrationProductsByGender(products, filterByGender);
            products = FiltrationProductsByName(products, filterByName);
            products = FiltrationProductsByDecreasePrice(products, isFilterByDecreasePrice);
            products = FiltrationProductsByIncreasePrice(products, isFilterByIncreasePrice);
            
            return products;
        }

        public async Task<IEnumerable<ProductCartDto>> GetAllProducts(
            string productType, 
            int? filterByMinPrice, 
            int? filterByMaxPrice, 
            int? filterByGender,
            string? filterByName,
            bool isFilterByDecreasePrice,
            bool isFilterByIncreasePrice)
        {
            var products = await _context.Products
                .Include(_ => _.ProductImages)
                .Include(_ => _.ProductType)
                .Where(_ => _.ProductType.Name == productType)
                .ToListAsync();

            products = FiltrationProducts(products, filterByMinPrice,
                filterByMaxPrice, filterByGender, filterByName, isFilterByDecreasePrice, isFilterByIncreasePrice);
            
            var productCarts = _mapper.Map<IEnumerable<ProductCartDto>>(products);
            
            return productCarts;
        }

        private async Task<Product> GetProductWithImagesAndSizes(int productId)
        {
            return await _context.Products
                .Include(_ => _.ProductImages)
                .Include(_ => _.ProductType)
                .ThenInclude(_ => _.SizeTypeToProductTypes)
                .ThenInclude(_ => _.SizeType)
                .FirstOrDefaultAsync(_ => _.ProductId == productId);
        }

        public async Task<GetProductDto> GetProduct(int productId)
        {
            var product = await GetProductWithImagesAndSizes(productId);
            var getProductDto = _mapper.Map<GetProductDto>(product);

            return getProductDto;
        }

        private async Task<IEnumerable<Product>> GetProductsBuUserId(int userId)
        {
            return await _context.Products
                .Include(_ => _.ProductImages)
                .Where(_ => _.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductCartDto>> GetSellerProducts(int userId)
        {
            var products = await GetProductsBuUserId(userId);
            var productsCartDto = _mapper.Map<IEnumerable<ProductCartDto>>(products);

            return productsCartDto;
        }
    }
}

﻿using SatchelAPI.Application.Dto;

namespace SatchelAPI.Interfaces.ServicesInterfaces;

public interface IProductService
{
    public Task AddProduct(ProductDto productDto, ICollection<ProductImageDto> addProductImagesDto);
    public Task DeleteProduct(int productId);
    public Task UpdateProduct(int productId, ProductDto productDto, ICollection<ProductImageDto> productImageDto);
}
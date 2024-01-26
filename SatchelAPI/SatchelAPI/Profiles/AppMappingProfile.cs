using AutoMapper;
using Satchel.Application.Models;
using SatchelAPI.Application.Dto;
using SatchelAPI.Application.Models;

namespace SatchelAPI.Profiles;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<ShoppingCart, ProductCartDto>()
            .ForMember(_ => _.ProductId, _ => _.MapFrom((s => s.ProductId)))
            .ForMember(_ => _.Name, _ => _.MapFrom(s => s.Product.Name))
            .ForMember(_ => _.Price, _ => _.MapFrom(s => s.Product.Price))
            .ForMember(_ => _.Images,
    _ => _.MapFrom(s => s.Product.ProductImages!
                    .Where(e => e.ProductId == s.ProductId)
                    .Select(e => e.ImagePath)))
            .ForMember(_ => _.SizeName, _ => _.MapFrom(s => s.SizeType.Name));

        CreateMap<ProductDto, Product>()
            .ForMember(_ => _.Name, _ => _.MapFrom(s => s.Name))
            .ForMember(_ => _.Description, _ => _.MapFrom(s => s.Description))
            .ForMember(_ => _.Price, _ => _.MapFrom(s => s.Price))
            .ForMember(_ => _.ProductTypeId, _ => _.MapFrom(s => s.ProductTypeId))
            .ForMember(_ => _.BrandTypeId, _ => _.MapFrom(s => s.BrandTypeId))
            .ForMember(_ => _.GenderTypeId, _ => _.MapFrom(s => s.GenderTypeId));

        CreateMap<ProductImageDto, ProductImages>()
            .ForMember(_ => _.ProductId, _ => _.MapFrom(s => s.ProductId))
            .ForMember(_ => _.ImagePath, _ => _.MapFrom(s => s.ImagePath));

        CreateMap<UserDTO, User>()
            .ForMember(_ => _.Email, _ => _.MapFrom(s => s.Email))
            .ForMember(_ => _.Password, _ => _.MapFrom(s => s.Password))
            .ForMember(_ => _.UserTypeId, _ => _.MapFrom(s => s.UserTypeId));

        CreateMap<User, GetUserDTO>()
            .ForMember(_ => _.UserId, _ => _.MapFrom(s => s.UserId))
            .ForMember(_ => _.Email, _ => _.MapFrom(s => s.Email))
            .ForMember(_ => _.UserTypeName, _ => _.MapFrom(s => s.UserType.Name));

        CreateMap<Product, GetProductDto>()
            .ForMember(_ => _.Id, _ => _.MapFrom(s => s.ProductId))
            .ForMember(_ => _.Name, _ => _.MapFrom(s => s.Name))
            .ForMember(_ => _.Price, _ => _.MapFrom(s => s.Price))
            .ForMember(_ => _.ProductTypeId, _ => _.MapFrom(s => s.ProductTypeId))
            .ForMember(_ => _.BrandTypeId, _ => _.MapFrom(s => s.BrandTypeId))
            .ForMember(_ => _.GenderTypeId, _ => _.MapFrom(s => s.GenderTypeId))
            .ForMember(_ => _.Images, _ => _.MapFrom(s => s.ProductImages.Select(_ => _.ImagePath)))
            .ForMember(_ => _.Sizes,
                _ => _.MapFrom(s => s.ProductType.SizeTypeToProductTypes
                    .Select(_ => _.SizeType.Name)));

        CreateMap<User, GetViewUserDto>()
            .ForMember(_ => _.FirstName, _ => _.MapFrom(s => s.FirstName))
            .ForMember(_ => _.MiddleName, _ => _.MapFrom(s => s.MiddleName))
            .ForMember(_ => _.LastName, _ => _.MapFrom(s => s.LastName))
            .ForMember(_ => _.Email, _ => _.MapFrom(s => s.Email))
            .ForMember(_ => _.DateOfBirth, _ => _.MapFrom(s => s.Birthday))
            .ForMember(_ => _.UserPhotoSrc, _ => _.MapFrom(s => s.Image))
            .ForMember(_ => _.UserType, _ => _.MapFrom(s => s.UserType.Name));

        CreateMap<ProductType, GetProductTypeDto>()
            .ForMember(_ => _.ProductTypeId, _ => _.MapFrom(s => s.ProductTypeId))
            .ForMember(_ => _.Name, _ => _.MapFrom(s => s.Name));

        CreateMap<BrandType, GetBrandTypeDto>()
            .ForMember(_ => _.BrandTypeId, _ => _.MapFrom(s => s.BrandTypeId))
            .ForMember(_ => _.Name, _ => _.MapFrom(s => s.Name));
    }
}
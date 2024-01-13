using AutoMapper;
using Satchel.Application.Models;
using SatchelAPI.Application.Dto;
using SatchelAPI.Application.Models;

namespace SatchelAPI.Profiles;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Product, ProductCartDto>()
            .ForMember(_ => _.ProductId, _ => _.MapFrom((s => s.ProductId)))
            .ForMember(_ => _.Name, _ => _.MapFrom(s => s.Name))
            .ForMember(_ => _.Price, _ => _.MapFrom(s => s.Price))
            .ForMember(_ => _.Images,
    _ => _.MapFrom(s => s.ProductImages!
                    .Where(e => e.ProductId == s.ProductId)
                    .Select(e => e.ImagePath)));

        CreateMap<ProductDto, Product>()
            .ForMember(_ => _.Name, _ => _.MapFrom(s => s.Name))
            .ForMember(_ => _.Description, _ => _.MapFrom(s => s.Description))
            .ForMember(_ => _.Price, _ => _.MapFrom(s => s.Price))
            .ForMember(_ => _.ProductTypeId, _ => _.MapFrom(s => s.ProductTypeId))
            .ForMember(_ => _.BrandTypeId, _ => _.MapFrom(s => s.BrandTypeId))
            .ForMember(_ => _.GenderTypeId, _ => _.MapFrom(s => s.GenderTypeId))
            .ForMember(_ => _.SizeTypeId, _ => _.MapFrom(s => s.SizeTypeId));

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
    }
}
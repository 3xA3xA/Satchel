using AutoMapper;
using Satchel.Application.Models;
using SatchelAPI.Application.Dto;

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
    }
}
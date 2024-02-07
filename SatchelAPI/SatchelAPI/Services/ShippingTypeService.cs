using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Satchel.Infrastructure;
using SatchelAPI.Application.Dto;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Services;

public class ShippingTypeService : IShippingTypeService
{
    private readonly SatchelDbContext _context;
    private readonly IMapper _mapper;
    
    public ShippingTypeService(SatchelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetShippingTypeDto>> GetShippingTypes()
    {
        var shippingTypes = await _context.ShippingTypes.ToListAsync();
        var getShippingTypeDtos = _mapper.Map<IEnumerable<GetShippingTypeDto>>(shippingTypes);

        return getShippingTypeDtos;
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Satchel.Infrastructure;
using SatchelAPI.Application.Dto;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Services;

public class ProductTypeService : IProductTypeService
{
    private readonly SatchelDbContext _context;
    private readonly IMapper _mapper;
    
    public ProductTypeService(SatchelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetProductTypeDto>> GetProductTypes()
    {
        var productTypes = await _context.ProductTypes.ToListAsync();
        var getProductTypesDto = _mapper.Map<IEnumerable<GetProductTypeDto>>(productTypes);

        return getProductTypesDto;
    }
}
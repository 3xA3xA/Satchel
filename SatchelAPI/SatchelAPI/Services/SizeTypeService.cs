using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Satchel.Application.Models;
using Satchel.Infrastructure;
using SatchelAPI.Application.Dto;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Services;

public class SizeTypeService : ISizeTypeService
{
    private readonly SatchelDbContext _context;
    private readonly IMapper _mapper;
    
    public SizeTypeService(SatchelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetSizeTypeDto>> GetSizeTypesByProductType(string productTypeName)
    {
        var sizeTypes = await _context.ProductTypes
            .Where(_ => _.Name == productTypeName)
            .Select(_ => _.SizeTypeToProductTypes.Select(_ => _.SizeType))
            .FirstOrDefaultAsync();

        var getSizeTypesDto = _mapper.Map<IEnumerable<GetSizeTypeDto>>(sizeTypes);

        return getSizeTypesDto;
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Satchel.Infrastructure;
using SatchelAPI.Application.Dto;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Services;

public class BrandService : IBrandService
{
    private readonly SatchelDbContext _context;
    private readonly IMapper _mapper;
    
    public BrandService(SatchelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetBrandTypeDto>> GetBrandTypes()
    {
        var brandTypes = await _context.BrandTypes.ToListAsync();
        var getBrandTypesDto = _mapper.Map<IEnumerable<GetBrandTypeDto>>(brandTypes);

        return getBrandTypesDto;
    }
}
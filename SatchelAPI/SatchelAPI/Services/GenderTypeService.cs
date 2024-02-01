using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Satchel.Infrastructure;
using SatchelAPI.Application.Dto;
using SatchelAPI.Interfaces;

namespace SatchelAPI.Services;

public class GenderTypeService : IGenderTypeService
{
    private readonly SatchelDbContext _context;
    private readonly IMapper _mapper;

    public GenderTypeService(SatchelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetGenderTypeDto>> GetGenderTypes()
    {
        var genderTypes = await _context.GenderTypes.ToListAsync();
        var getGenderTypesDto = _mapper.Map<IEnumerable<GetGenderTypeDto>>(genderTypes);

        return getGenderTypesDto;
    }
}
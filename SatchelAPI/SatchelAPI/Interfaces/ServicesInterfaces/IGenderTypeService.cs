using SatchelAPI.Application.Dto;

namespace SatchelAPI.Interfaces;

public interface IGenderTypeService
{
    public Task<IEnumerable<GetGenderTypeDto>> GetGenderTypes();
}
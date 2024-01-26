using SatchelAPI.Application.Dto;

namespace SatchelAPI.Interfaces.ServicesInterfaces;

public interface IBrandService
{
    public Task<IEnumerable<GetBrandTypeDto>> GetBrandTypes();
}
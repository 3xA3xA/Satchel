using SatchelAPI.Application.Dto;

namespace SatchelAPI.Interfaces.ServicesInterfaces;

public interface IProductTypeService
{
    public Task<IEnumerable<GetProductTypeDto>> GetProductTypes();
}
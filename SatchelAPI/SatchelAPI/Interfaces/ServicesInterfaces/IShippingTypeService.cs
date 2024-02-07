using SatchelAPI.Application.Dto;

namespace SatchelAPI.Interfaces.ServicesInterfaces;

public interface IShippingTypeService
{
    public Task<IEnumerable<GetShippingTypeDto>> GetShippingTypes();
}
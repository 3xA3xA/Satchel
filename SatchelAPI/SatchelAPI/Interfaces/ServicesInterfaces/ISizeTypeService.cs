using Satchel.Application.Models;
using SatchelAPI.Application.Dto;

namespace SatchelAPI.Interfaces.ServicesInterfaces;

public interface ISizeTypeService
{
    public Task<IEnumerable<GetSizeTypeDto>> GetSizeTypesByProductType(string productTypeName);
}
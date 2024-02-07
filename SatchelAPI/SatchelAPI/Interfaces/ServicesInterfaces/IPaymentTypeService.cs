using SatchelAPI.Application.Dto;

namespace SatchelAPI.Interfaces.ServicesInterfaces;

public interface IPaymentTypeService
{
    public Task<IEnumerable<GetPaymentTypeDto>> GetPaymentTypes();
}
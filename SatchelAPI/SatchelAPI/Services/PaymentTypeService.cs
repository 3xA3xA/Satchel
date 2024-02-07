using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Satchel.Infrastructure;
using SatchelAPI.Application.Dto;
using SatchelAPI.Interfaces.ServicesInterfaces;

namespace SatchelAPI.Services;

public class PaymentTypeService : IPaymentTypeService
{
    private readonly SatchelDbContext _context;
    private readonly IMapper _mapper;
    
    public PaymentTypeService(SatchelDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetPaymentTypeDto>> GetPaymentTypes()
    {
        var paymentTypes = await _context.PaymentTypes.ToListAsync();
        var paymentTypeDtos = _mapper.Map<IEnumerable<GetPaymentTypeDto>>(paymentTypes);

        return paymentTypeDtos;
    }
}
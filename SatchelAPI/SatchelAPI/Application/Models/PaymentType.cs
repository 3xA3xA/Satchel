using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class PaymentType
{
    [Key]
    public required int PaymentTypeId { get; set; }
    [MaxLength(100)]
    public required string Name { get; set; }

    [InverseProperty(nameof(Order.PaymentType))]
    public required ICollection<Order> Orders { get; set; }
}
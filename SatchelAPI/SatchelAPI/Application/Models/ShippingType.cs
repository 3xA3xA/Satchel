using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class ShippingType
{
    [Key]
    public required int ShippingTypeId { get; set; }
    [MaxLength(100)]
    public required string Name { get; set; }
    public required int Days { get; set; }

    [InverseProperty(nameof(Order.ShippingType))]
    public required ICollection<Order> Orders { get; set; }
}
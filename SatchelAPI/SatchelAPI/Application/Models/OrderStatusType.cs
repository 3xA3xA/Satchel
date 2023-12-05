using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class OrderStatusType
{
    [Key]
    public required int OrderStatusTypeId { get; set; }
    [MaxLength(100)]
    public required string Name { get; set; }

    [InverseProperty(nameof(Order.OrderStatusType))]
    public required ICollection<Order> Orders { get; set; }
}
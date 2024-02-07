using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class ShippingType
{
    [Key]
    [Required]
    public int ShippingTypeId { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int Days { get; set; }

    [InverseProperty(nameof(Order.ShippingType))]
    public ICollection<Order> Orders { get; set; }
}
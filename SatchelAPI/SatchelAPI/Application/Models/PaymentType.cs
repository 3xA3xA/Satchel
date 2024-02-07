using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class PaymentType
{
    [Key]
    [Required]
    public int PaymentTypeId { get; set; }
    
    [Required]
    public string Name { get; set; }

    [InverseProperty(nameof(Order.PaymentType))]
    public ICollection<Order> Orders { get; set; }
}
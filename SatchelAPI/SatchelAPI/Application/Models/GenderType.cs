using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class GenderType
{
    [Key]
    public required int GenderTypeId { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }

    [InverseProperty(nameof(Product.GenderType))]
    public required ICollection<Product> Products { get; set; }
}
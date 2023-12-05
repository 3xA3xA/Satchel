using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class BrandType
{
    [Key]
    public required int BrandTypeId { get; set; }
    public string? Image { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }

    [InverseProperty(nameof(Product.BrandType))]
    public required ICollection<Product> Products { get; set; }
}
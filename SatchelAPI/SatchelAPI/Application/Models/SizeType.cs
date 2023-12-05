using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class SizeType
{
    [Key]
    public required int SizeTypeId { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }

    [InverseProperty(nameof(Product.SizeType))]
    public required ICollection<Product> Products { get; set; }

    [InverseProperty(nameof(SizeTypeToProductType.SizeType))]
    public required ICollection<SizeTypeToProductType> sizeTypeToProductTypes { get; set; }
}
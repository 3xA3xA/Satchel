using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class ProductType
{
    [Key]
    public int ProductTypeId { get; set; }

    public int? ParentProductTypeId { get; set; }

    [MaxLength(255)]
    public required string Name { get; set; }

    [ForeignKey(nameof(ParentProductTypeId))]
    public ProductType? ParentProductType { get; set; }

    [InverseProperty(nameof(Product.ProductType))]
    public required ICollection<Product> Products { get; set; }

    [InverseProperty(nameof(SizeTypeToProductType.ProductType))]
    public required ICollection<SizeTypeToProductType> SizeTypeToProductTypes { get; set; }
}
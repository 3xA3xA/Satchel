using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class SizeType
{
    [Key]
    [Required] public int SizeTypeId { get; set; }
    [MaxLength(255)]
    [Required] public string Name { get; set; }

    [InverseProperty(nameof(ShoppingCart.SizeType))]
    public required ICollection<ShoppingCart> ShoppingCarts { get; set; }

    [InverseProperty(nameof(SizeTypeToProductType.SizeType))]
    public required ICollection<SizeTypeToProductType> SizeTypeToProductTypes { get; set; }
}
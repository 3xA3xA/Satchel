using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Satchel.Application.Models;

namespace SatchelAPI.Application.Models;

public class SizeTypeToProduct
{
    public SizeTypeToProduct(int sizeTypeId, int productId)
    {
        SizeTypeId = sizeTypeId;
        ProductId = productId;
    }
    
    [Key]
    [Required]
    public int SizeTypeToProductId { get; set; }
    [Required] public int SizeTypeId { get; set; }
    [Required] public int ProductId { get; set; }
    
    [ForeignKey(nameof(SizeTypeId))]
    public SizeType SizeType { get; set; }
    
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }
}
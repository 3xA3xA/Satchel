using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class SizeTypeToProductType
{
    [Key]
    [Required]
    public int SizeTypeToProductTypeId { get; set; }
    
    [Required]
    public int SizeTypeId { get; set; }
    
    [Required]
    public int ProductTypeId { get; set; }
    
    [ForeignKey(nameof(SizeTypeId))]
    public SizeType SizeType { get; set; }
    
    [ForeignKey(nameof(ProductTypeId))]
    public ProductType ProductType { get; set; }
}
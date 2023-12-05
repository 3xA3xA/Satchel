using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class SizeTypeToProductType
{
    [Key]
    public required int SizeTypeToProductTypeId { get; set; }
    public required int SizeTypeId { get; set; }
    public required int ProductTypeId { get; set; }

    [ForeignKey((nameof(SizeTypeId)))]
    public required SizeType SizeType { get; set; }

    [ForeignKey(nameof(ProductTypeId))]
    public required ProductType ProductType { get; set; }
}
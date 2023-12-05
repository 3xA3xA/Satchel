using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class Order
{
    [Key]
    public required int OrderId { get; set; }
    public required int ProductId { get; set; }
    public required int UserId { get; set; }
    public required int OrderStatusTypeId { get; set; }
    public required int PaymentTypeId { get; set; }
    public required int ShippingTypeId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public required Product Product { get; set; }

    [ForeignKey(nameof(UserId))]
    public required User User { get; set; }

    [ForeignKey(nameof(OrderStatusTypeId))]
    public required OrderStatusType OrderStatusType { get; set; }

    [ForeignKey(nameof(PaymentTypeId))]
    public required PaymentType PaymentType { get; set; }

    [ForeignKey(nameof(ShippingTypeId))]
    public required ShippingType ShippingType { get; set; }
}
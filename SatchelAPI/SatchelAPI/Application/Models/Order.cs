using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class Order
{
    public Order(
        int productId, 
        int userId, 
        int orderStatusTypeId, 
        int paymentTypeId, 
        int shippingTypeId,
        int receiptCode)
    {
        ProductId = productId;
        UserId = userId;
        OrderStatusTypeId = orderStatusTypeId;
        PaymentTypeId = paymentTypeId;
        ShippingTypeId = shippingTypeId;
        Date = DateTime.UtcNow;
        ReceiptCode = receiptCode;
    }
    
    [Key]
    [Required]
    public int OrderId { get; set; }
    
    [Required]
    public int ProductId { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int OrderStatusTypeId { get; set; }
    
    [Required]
    public int PaymentTypeId { get; set; }
    
    [Required]
    public int ShippingTypeId { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public int ReceiptCode { get; set; } 

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    [ForeignKey(nameof(OrderStatusTypeId))]
    public OrderStatusType OrderStatusType { get; set; }

    [ForeignKey(nameof(PaymentTypeId))]
    public PaymentType PaymentType { get; set; }

    [ForeignKey(nameof(ShippingTypeId))]
    public ShippingType ShippingType { get; set; }
}
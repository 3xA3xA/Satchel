using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class ShoppingCart
{
    public ShoppingCart(int productId, int userId, int sizeTypeId)
    {
        ProductId = productId;
        UserId = userId;
        SizeTypeId = sizeTypeId;
    }
    
    [Key] [Required] public int ShoppingCartId { get; set; }
    [Required] public int ProductId { get; set; }
    [Required] public int UserId { get; set; }
    [Required] public int SizeTypeId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    [ForeignKey(nameof(SizeTypeId))]
    public SizeType SizeType { get; set; }
}
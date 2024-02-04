using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SatchelAPI.Application.Models;

namespace Satchel.Application.Models;
public class Product
{
    [Key]
    [Required]
    public int ProductId { get; set; }
    [MaxLength(50)]
    [Required]
    public string Name { get; set; }
    [MaxLength(1000)]
    [Required]
    public string Description { get; set; }
    [Required] public int ProductTypeId { get; set; }

    [Precision(18, 2)]
    [Required]
    public decimal Price { get; set; }
    [Required] public int BrandTypeId { get; set; }
    [Required] public int GenderTypeId { get; set; }
    public int? UserId { get; set; }

    [ForeignKey(nameof(ProductTypeId))]
    public ProductType ProductType { get; set; }

    [ForeignKey(nameof(BrandTypeId))]
    public BrandType BrandType { get; set; }

    [ForeignKey(nameof(GenderTypeId))]
    public GenderType GenderType { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    
    [InverseProperty(nameof(Feedback.Product))]
    public ICollection<Feedback> Feedbacks { get; set; }

    [InverseProperty(nameof(Order.Product))]
    public ICollection<Order> Orders { get; set; }

    [InverseProperty(nameof(Models.Favourites.Product))]
    public ICollection<Favourites> Favourites { get; set; }

    [InverseProperty(nameof(ShoppingCart.Product))]
    public ICollection<ShoppingCart> ShoppingCarts { get; set; }
    
    [InverseProperty(nameof(SizeTypeToProduct.Product))]
    public required ICollection<SizeTypeToProduct> SizeTypeToProducts { get; set; }
    
    [InverseProperty(nameof(SatchelAPI.Application.Models.ProductImages.Product))]
    public ICollection<ProductImages>? ProductImages { get; set; }
}
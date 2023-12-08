using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SatchelAPI.Application.Models;

namespace Satchel.Application.Models;
public class Product
{
    [Key]
    public required int ProductId { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; }
    [MaxLength(1000)]
    public required string Description { get; set; }
    public required int ProductTypeId { get; set; }

    [Precision(18, 2)]
    public required decimal Price { get; set; }
    public ICollection<ProductImages>? ProductImages { get; set; }
    public required int BrandTypeId { get; set; }
    public required int GenderTypeId { get; set; }
    public required int SizeTypeId { get; set; }

    [ForeignKey(nameof(ProductTypeId))]
    public required ProductType ProductType { get; set; }

    [ForeignKey(nameof(BrandTypeId))]
    public required BrandType BrandType { get; set; }

    [ForeignKey(nameof(GenderTypeId))]
    public required GenderType GenderType { get; set; }

    [ForeignKey(nameof(SizeTypeId))]
    public required SizeType SizeType { get; set; }

    [InverseProperty(nameof(Feedback.Product))]
    public required ICollection<Feedback> Feedbacks { get; set; }

    [InverseProperty(nameof(Order.Product))]
    public required ICollection<Order> Orders { get; set; }

    [InverseProperty(nameof(Models.Favourites.Product))]
    public required ICollection<Favourites> Favourites { get; set; }

    [InverseProperty(nameof(ShoppingCart.Product))]
    public required ICollection<ShoppingCart> ShoppingCarts { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    public int UserTypeId { get; set; }
    [MaxLength(150)]
    public required string Email { get; set; }
    [MaxLength(150)]
    public required string Password { get; set; }

    [ForeignKey(nameof(UserTypeId))]
    public required UserType UserType { get; set; }

    [InverseProperty(nameof(Feedback.User))]
    public required ICollection<Feedback> Feedbacks { get; set; }

    [InverseProperty(nameof(Order.User))]
    public required ICollection<Order> Orders { get; set; }

    [InverseProperty(nameof(Models.Favourites.User))]
    public required ICollection<Favourites> Favourites { get; set; }

    [InverseProperty(nameof(ShoppingCart.User))]
    public required ICollection<ShoppingCart> ShoppingCarts { get; set; }
}
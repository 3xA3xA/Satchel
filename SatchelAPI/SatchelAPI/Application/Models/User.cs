using SatchelAPI.Application.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    public int UserTypeId { get; set; }
    [MaxLength(150)]
    [Required]
    public string Email { get; set; }
    [MaxLength(150)]
    [Required]
    public string Password { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime? Birthday { get; set; }

    [ForeignKey(nameof(UserTypeId))]
    [Required]
    public UserType UserType { get; set; }

    [InverseProperty(nameof(Feedback.User))]
    public ICollection<Feedback> Feedbacks { get; set; }

    [InverseProperty(nameof(Order.User))]
    public ICollection<Order> Orders { get; set; }

    [InverseProperty(nameof(Models.Favourites.User))]
    public ICollection<Favourites> Favourites { get; set; }

    [InverseProperty(nameof(ShoppingCart.User))]
    public ICollection<ShoppingCart> ShoppingCarts { get; set; }
}
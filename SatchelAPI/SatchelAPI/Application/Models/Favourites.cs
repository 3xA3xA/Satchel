using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class Favourites
{
    [Key]
    public required int FavouritesId { get; set; }
    public required int ProductId { get; set; }
    public required int UserId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public required Product Product { get; set; }

    [ForeignKey(nameof(UserId))]
    public required User User { get; set; }
}
﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Satchel.Application.Models;

public class Favourites
{
    public Favourites(int productId, int userId)
    {
        ProductId = productId;
        UserId = userId;
    }
    
    [Key]
    [Required] public int FavouritesId { get; set; }
    [Required] public int ProductId { get; set; }
    [Required] public int UserId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}
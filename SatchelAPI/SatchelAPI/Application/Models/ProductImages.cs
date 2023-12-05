using Satchel.Application.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SatchelAPI.Application.Models
{
    public class ProductImages
    {
        [Key]
        public int ImageId { get; set; }

        public required int ProductId { get; set; }

        [MaxLength(255)]
        public required string ImagePath { get; set; }

        [ForeignKey(nameof(ProductId))]
        public required Product Product { get; set; }
    }
}

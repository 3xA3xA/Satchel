using Satchel.Application.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SatchelAPI.Application.Models
{
    public class ProductImages
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public int ProductId { get; set; }
        
        [Required]
        public string ImagePath { get; set; }

        [ForeignKey(nameof(ProductId))]
        public required Product Product { get; set; }
    }
}

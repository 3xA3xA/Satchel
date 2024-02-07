using Satchel.Application.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SatchelAPI.Application.Models
{
    public class ProductImages
    {
        public ProductImages(int productId, string imagePath)
        {
            ProductId = productId;
            ImagePath = imagePath;
        }

        [Key]
        public int ImageId { get; set; }

        [Required]
        public int ProductId { get; set; }
        
        [Required]
        public string ImagePath { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}

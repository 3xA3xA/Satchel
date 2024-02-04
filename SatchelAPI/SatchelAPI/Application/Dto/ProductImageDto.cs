namespace SatchelAPI.Application.Dto;

public class ProductImageDto
{
    public ProductImageDto(int? productId, string imagePath)
    {
        ProductId = productId;
        ImagePath = imagePath;
    }
    public int? ProductId { get; set; } // Для добавления продукта передавать это поле не нужно, оно потом вставляется 
    public string ImagePath { get; set; }
}
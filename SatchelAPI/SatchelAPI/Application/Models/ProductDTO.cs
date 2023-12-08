namespace SatchelAPI.Application.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
        public string Description { get; set; }
        public int? ProductTypeId { get; set; }
        public int? BrandTypeId { get; set; }
        public int? GenderTypeId { get; set; }
        public List<string> Sizes { get; set; }
    }
}

namespace Arty.Dtos
{
    public class ProductsDto
    {
        public string ProductName { get; set; } = null!;

        public string? ProductDescription { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int? SubCategoryId { get; set; }
    }
}

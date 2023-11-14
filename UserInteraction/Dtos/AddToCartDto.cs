namespace Arty.Dtos
{
    public class AddToCartDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; } = 1;
    }
}

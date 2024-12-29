namespace BookShop.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> CartItems { get; set; }

        public int UserId { get; set; }
        public decimal?TotalPrice { get; set; }
        public int TotalQuantity { get; set; }

    }
}

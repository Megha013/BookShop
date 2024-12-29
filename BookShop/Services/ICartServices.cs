using BookShop.Models;

namespace BookShop.Services
{
    public interface ICartServices
    {
        public IEnumerable<ShoppingCartItem> GetCarts();

        public IEnumerable<ShoppingCartItem> GetCartById(int id);

        public int AddtoCart(ShoppingCartItem cart);

        public int Delete(int id);
        object Update(Cart cart);
        void AddtoCart(Cart cart);
    }
}

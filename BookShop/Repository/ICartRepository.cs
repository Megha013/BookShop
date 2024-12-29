using BookShop.Models;

namespace BookShop.Repository
{
    public interface ICartRepository
    {
        public IEnumerable<ShoppingCartItem> GetCarts();

        public IEnumerable<ShoppingCartItem> GetCartById(int id);

        public int AddtoCart(ShoppingCartItem cart);    
        public int Delete(int id);
       
    }
}

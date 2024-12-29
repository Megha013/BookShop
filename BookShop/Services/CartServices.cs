using BookShop.Models;
using BookShop.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Services
{
    public class CartServices:ICartServices
    {
        private readonly ICartRepository repo;

        public CartServices(ICartRepository repo)
        {
            this.repo = repo;
        }

        public int AddtoCart(ShoppingCartItem cart)
        {
            return repo.AddtoCart(cart);
        }

        public void AddtoCart(Cart cart)
        {
            throw new NotImplementedException();
        }

        //public void AddtoCart(Cart cart)
        //{
        //    throw new NotImplementedException();
        //}

        public int Delete(int id)
        {
            return repo.Delete(id);
        } 

        public IEnumerable<ShoppingCartItem> GetCartById(int id)
        {
            return repo.GetCartById(id);
        }

        public IEnumerable<ShoppingCartItem> GetCarts()
        {
            return repo.GetCarts();
        }

        public object Update(Cart cart)
        {
            throw new NotImplementedException();
        }

        //public object Update(Cart cart)
        //{
        //    return repo.Update(cart);
        //}
    }
}

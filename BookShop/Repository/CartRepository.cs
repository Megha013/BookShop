using BookShop.Data;
using BookShop.Models;

namespace BookShop.Repository
{
    public class CartRepository:ICartRepository
    {
        private readonly ApplicationDbContext db;

        public CartRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int AddtoCart(ShoppingCartItem cart)
        {
            int result = 0;
            db.Carts.Add(cart);
            result = db.SaveChanges();
            return result;
        }

        public int Delete(int id)
        {
            int result = 0;
            var res = db.Carts.Where(x => x.CartId == id).SingleOrDefault();
            if (res != null)
            {
                db.Carts.Remove(res);
                result = db.SaveChanges();
            }
            return result;
        }

        public int DeleteAll(IEnumerable<ShoppingCartItem> carts)
        {
            int result = 0;
            db.Carts.RemoveRange(carts);
            result = db.SaveChanges();
            return result;
        }

      

        public IEnumerable<ShoppingCartItem> GetCartById(int id)
        {
            var res = from c in db.Carts
                      join u in db.Users on c.UserId equals u.UserId
                      join b in db.Books
                      on c.BookId equals b.BookId
                      where c.UserId == id
                      select new ShoppingCartItem
                      {
                          CartId = c.CartId,
                          UserId = c.UserId,
                          BookId = c.BookId,
                          Quantity = c.Quantity,
                        
                      };
            return res;
        }

        public IEnumerable<ShoppingCartItem> GetCarts()
        {
            var res = from c in db.Carts
                      join u in db.Users on c.UserId equals u.UserId
                      join b in db.Books
                      on c.BookId equals b.BookId
                      select new ShoppingCartItem
                      {
                          CartId = c.CartId,
                          UserId = c.UserId,
                          BookId = c.BookId,
                          Quantity = c.Quantity
                         
                      };
            return res;
        }

        //public object Update(Cart cart)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

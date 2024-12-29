using BookShop.Data;
using BookShop.Models;
using BookShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly ICartServices cartservices;
        private readonly IBookServices bookservices;

        private List<ShoppingCartItem> _cartItem;
        public ShoppingCartController(ApplicationDbContext db, ICartServices cartservices, IBookServices bookservices)
        {
            this.db = db;
            this.cartservices = cartservices;
            this.bookservices = bookservices;
            _cartItem = new List<ShoppingCartItem>();
        }


        public IActionResult AddToCart(int id, int quantity=1)
        {
            var bookToCart = db.Books.Find(id);
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();
            var existingCartItem = cartItems.FirstOrDefault(item => item.Book.BookId == id);
            int userid = (int)HttpContext.Session.GetInt32("UserId");


            //if (existingCartItem != null)
            //{
            //    existingCartItem.Quantity++;
            //}
            //else
            //{
            //    cartItems.Add(new ShoppingCartItem
            //    {
            //        Book = bookToCart,
            //        UserId = userid,
            //        Quantity = 1

            //    });
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }
            else
            {
                cartItems.Add(new ShoppingCartItem
                {
                    Book = bookToCart,
                    UserId = userid,
                    Quantity = quantity
                });
                //var result = (from o in db.OrderDetails
                //              join u in db.Users on o.UserId equals u.UserId
                //              join b in db.Books on o.BookId equals b.BookId
                //              where o.BookId==id
                //              select new Orderdetails
                //              {
                //                  BookId = o.BookId,
                //                  BookName = o.BookName,
                //                  UserId = o.UserId,
                //                  UserName = o.UserName,
                //                  Quantity = o.Quantity,
                //                  TotalPrice = o.TotalPrice,
                //                  OrderDate = o.OrderDate,
                //              }).ToList();
                //return View(result);
            }
            HttpContext.Session.Set("Cart", cartItems);

            return RedirectToAction("ViewCart");

        }
        [HttpGet]
        public IActionResult ViewCart()
        {
            int userid = (int)HttpContext.Session.GetInt32("UserId");
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var cartItem = new ShoppingCart
            {
                CartItems = cartItems,
                UserId = userid,
                TotalPrice = cartItems.Sum(item => item.Book.Price * item.Quantity)
            };
            HttpContext.Session.Set("Cart", cartItems);
            return View(cartItem);
        }

        public IActionResult RemoveItems(int id)
        {
            int userid = (int)HttpContext.Session.GetInt32("UserId");
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();
            var itemToRemove = cartItems.FirstOrDefault(item => item.Book.BookId == id);

            if (itemToRemove != null)
            {
                if (itemToRemove.Quantity > 1)
                {
                    itemToRemove.Quantity--;
                }
                else
                {
                    cartItems.Remove(itemToRemove);
                }

            }
            HttpContext.Session.Set("Cart", cartItems);
            return RedirectToAction("ViewCart");

        }
        public IActionResult PurchaseItems()
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            foreach (var item in cartItems)
            {
                db.OrderDetails.Add(new Orderdetails
                {
                    BookId = item.Book.BookId,
                    //BookName=item.BookName,
                    Quantity = item.Quantity,
                    UserId = item.UserId,
                    OrderDate = DateTime.Now,
                    TotalPrice = item.Book.Price * item.Quantity

                });
            }
            db.SaveChanges();
            HttpContext.Session.Set("Cart", new List<ShoppingCartItem>());

            return RedirectToAction("HomePage");
        }
        //public IActionResult HomePage()
        //{
        //    var orders = db.OrderDetails
        //        .Select(order => new Orderdetails
        //        {
        //            BookId = order.BookId,
        //            UserId = order.UserId,
        //            Quantity = order.Quantity,
        //            TotalPrice = order.TotalPrice,
        //            OrderDate = order.OrderDate,
        //        })
        //        .ToList();
        //    return View(orders);
        //}
       
        public IActionResult HomePage()
        {
            var result = (from o in db.OrderDetails
                          join u in db.Users on o.UserId equals u.UserId
                          join b in db.Books on o.BookId equals b.BookId
                          select new Orderdetails
                          {
                              BookId = o.BookId,
                              BookName = b.BookName, 
                              UserId = o.UserId,
                              UserName = u.UserName, 
                              Quantity = o.Quantity,
                              TotalPrice = o.TotalPrice,
                              OrderDate = o.OrderDate,
                          }).ToList();

            return View(result);
        }

    }
}

using BookShop.Data;
using BookShop.Models;
using BookShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Security.Claims;

namespace BookShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices cart_services;
        private readonly IBookServices book_services;
        private Microsoft.AspNetCore.Hosting.IWebHostEnvironment env;
        private readonly ApplicationDbContext db;

        public CartController(ICartServices cart_services, IBookServices book_services, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env, ApplicationDbContext db)
        {
            this.cart_services = cart_services;
            this.book_services = book_services;
            this.env = env;
            this.db = db;
        }


        // GET: CartController
        public ActionResult Index()
        {
            int userid = (int)HttpContext.Session.GetInt32("UserId");
            var res = cart_services.GetCartById(userid);
            return View(res);
        }

        [HttpGet]
        public IActionResult AddToCart(int bookid)
        {
            try
            {
                if (bookid == 0)
                {
                    ViewBag.Error = "Invalid product ID.";
                    return RedirectToAction("HomePage", "Index");
                }

                int userid = (int)HttpContext.Session.GetInt32("UserId");

                var res = book_services.GetBookById(bookid);
                if (res != null)
                {
                    cart_services.AddtoCart(new Cart
                    {
                        BookId = res.BookId,
                        UserId = userid,
                        Price = res.Price,
                        BookName = res.BookName,
                        Quantity = 1
                    });
                    return RedirectToAction("Index", "Cart");
                }
                else
                {
                    ViewBag.Error = "Error to add product into cart !";
                    return RedirectToAction("HomePage", "Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("DisplayProduct", "DisplayProducts");

            }
        }

        public IActionResult UpdateQuantity(Cart cart)
        {
            try
            {
                int userid = (int)HttpContext.Session.GetInt32("UserId");
                var res = cart_services.Update(cart);
                if (res != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Error to  update quantity !";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index", "Cart");
            }
        }


        public IActionResult Delete(int id)
        {
            try
            {
                var res = cart_services.Delete(id);
                if (res >= 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Error to  delete cart !";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return RedirectToAction("Index", "Cart");
            }
        }

    }
}



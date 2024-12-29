using BookShop.Models;
using BookShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace BookShop.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices services;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserController(IUserServices services, IHttpContextAccessor httpContextAccessor)
        {
            this.services = services;
            this.httpContextAccessor = httpContextAccessor;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                user.RoleId = 2;
                var res = services.RegisterUser(user);
                if (res >= 1)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    ViewBag.ErrorMessage = "Something went wrong !";
                    return View("Create");
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            try
            {
                var res = services.ValidateUser(email, password);
                if (res != null)
                {
                    if (res.RoleId == 1)
                    {
                        HttpContext.Session.SetString("Admin", res.UserName);
                        return RedirectToAction("Index", "Book");
                    }
                    else
                    {
                        HttpContext.Session.SetString("User", res.UserName);
                        HttpContext.Session.SetInt32("UserId", res.UserId);
                        return RedirectToAction("Index", "HomePage");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Please check your Email,Password";

                    return RedirectToAction(nameof(Login));
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View();
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}
    
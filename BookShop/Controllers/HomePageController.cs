using BookShop.Models;
using BookShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    public class HomePageController : Controller
    {
        private readonly IBookServices services;
        public HomePageController(IBookServices services)
        { 
            this.services = services;
        }
        //public ActionResult Index()
        //{
        //    var model = services.GetBooks();
        //    return View(model);
        //}
        public ActionResult Index(int pg=1)
        {
            var model = services.GetBooks();
            const int pagesize = 20;
            if (pg < 1)
            {
                pg = 1;
            }
            int rescount = model.Count();
            var pager = new Pager(rescount, pg, pagesize);
            int recskip = (pg - 1) * pagesize;
            var data = model.Skip(recskip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(model);
        }

        // GET: HomePageController/Details/5
        public ActionResult Details(int id)
        {
            var book = services.GetBookById(id);
            return View(book);
        }

        // GET: HomePageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomePageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomePageController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomePageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomePageController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomePageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

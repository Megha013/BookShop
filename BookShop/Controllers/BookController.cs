using BookShop.Models;
using BookShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.OleDb;

namespace BookShop.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookServices services;
        private readonly ICategoryService cat;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        public BookController(IBookServices services,Microsoft.AspNetCore.Hosting.IHostingEnvironment env, ICategoryService cat)
        {
            this.services = services;
            this.env = env;
            this.cat = cat;
        }
        public ActionResult Index(int pg=1)
        {
            var books = services.GetBooks();
            const int pagesize = 5;
            if (pg < 1)
            {
                pg = 1;
            }

            int recscount = books.Count();

            var pager = new Pager(recscount, pg, pagesize);

            int recskip = (pg - 1) * pagesize;

            var data = books.Skip(recskip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;
            return View(data);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = services.GetBookById(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            ViewBag.Categories = cat.GetCategories();
            return View();

        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book,IFormFile file)
        {
            try
            {
                using(var fs=new FileStream(env.WebRootPath+"\\images\\"+file.FileName,FileMode.Create,FileAccess.Write))
                {
                    file.CopyTo(fs);
                }
                book.ImgUrl = "~/images/" + file.FileName;
                var result = services.AddBook(book);
                if(result>=1)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.Error = "Something went wrong";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = services.GetBookById(id);
            TempData["oldUrl"] = book.ImgUrl;
            TempData.Keep("oldUrl");
            return View(book);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book,IFormFile file)
        {
            try
            {
                string oldimageurl = TempData["oldUrl"].ToString();
                if (file != null)
                {
                    using (var fs = new FileStream(env.WebRootPath + "\\images\\" + file.FileName, FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fs);
                    }
                    book.ImgUrl = "~/images/" + file.FileName;

                    string[] str = oldimageurl.Split("/");
                    string str1 = (str[str.Length - 1]);
                    string path = env.WebRootPath + "\\images\\" + str1;
                    System.IO.File.Delete(path);
                }
                else
                {
                    book.ImgUrl = oldimageurl;
                }
                int res=services.UpdateBook(book);
                if(res==1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = services.GetBookById(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfrim(int id)
        {
            try
            {
                var p = services.GetBookById(id);

                    string[] str =p.ImgUrl.Split("/");
                    string str1 = (str[str.Length - 1]);
                    string path = env.WebRootPath + "\\images\\" + str1;
                    System.IO.File.Delete(path);
               
                int res = services.DeleteBook(id);
                if (res == 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Something went wrong";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}

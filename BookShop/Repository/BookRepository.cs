using BookShop.Data;
using BookShop.Models;

namespace BookShop.Repository
{
    public class BookRepository:IBookRepository
    {
        private readonly ApplicationDbContext db;

        public BookRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public int AddBook(Book book)
        {
            int result;
            db.Books?.Add(book);
            result = db.SaveChanges();
            return result;
        }
        public int DeleteBook(int id)
        {
            int result = 0;
            var b = db.Books?.Where(x => x.BookId == id).FirstOrDefault();
            if (b != null)
            {
                db.Books?.Remove(b);
                result = db.SaveChanges();
            }
            return result;

        }
        public Book GetBookById(int id)
        {
            {
                var res = (from b in db.Books
                           join c in db.Categories on b.CategoryId equals c.CategoryId
                           where b.BookId==id
                           select new Book
                           {
                               BookId = b.BookId,
                               BookName = b.BookName,
                               Description = b.Description,
                               Author = b.Author,
                               CategoryId = b.CategoryId,
                               CategoryName = c.CategoryName,
                               Price = b.Price,
                               ImgUrl = b.ImgUrl,
                           }).FirstOrDefault();
                return res;
            }

        }

        public Book GetBookByAuthor(string name)
        {
            var res = (from b in db.Books
                       join c in db.Categories on b.CategoryId equals c.CategoryId
                       where b.BookName==name
                       select new Book
                       {
                           BookId = b.BookId,
                           BookName = b.BookName,
                           Description = b.Description,
                           Author = b.Author,
                           CategoryId = b.CategoryId,
                           CategoryName = c.CategoryName,
                           Price = b.Price,
                           ImgUrl = b.ImgUrl,
                       }).FirstOrDefault();
            return res;

        }

        public IEnumerable<Book> GetBooks()
        {
            {
                var res = (from b in db.Books
                           join c in db.Categories on b.CategoryId equals c.CategoryId
                           select new Book
                           {
                               BookId = b.BookId,
                               BookName = b.BookName,
                               Description = b.Description,                              
                               Author=b.Author ,
                               CategoryId = b.CategoryId,
                               CategoryName = c.CategoryName,
                               Price = b.Price,
                               ImgUrl = b.ImgUrl,
                           }).ToList();
                return res;
            }

        }

        public int UpdateBook(Book book)
        {

            int result = 0;
            var b = db.Books?.Where(x => x.BookId == book.BookId).FirstOrDefault();
            if (b != null)
            {
                db.Entry<Book>(b).CurrentValues.SetValues(book);
                result = db.SaveChanges();
            }
            return result;
        }

    }
}

using BookShop.Models;
using BookShop.Repository;

namespace BookShop.Services
{
    public class BookServices:IBookServices
    {
        private readonly IBookRepository repo;
        public BookServices(IBookRepository repo)
        { this.repo = repo; }
        public int AddBook(Book book)
        {
            return repo.AddBook(book);
        }

        public int DeleteBook(int id)
        {
            return repo.DeleteBook(id);
        }

        public Book GetBookByAuthor(string name)
        {
            return repo.GetBookByAuthor(name);
        }

        public Book GetBookById(int id)
        {
            return repo.GetBookById(id);
        }

        public IEnumerable<Book> GetBooks()
        {
            return repo.GetBooks();
        }

        public int UpdateBook(Book book)
        {
            return repo.UpdateBook(book);
        }
    }
}

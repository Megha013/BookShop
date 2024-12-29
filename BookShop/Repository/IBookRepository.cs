using BookShop.Models;

namespace BookShop.Repository
{
    public interface IBookRepository
    { 
        IEnumerable<Book> GetBooks();
        Book GetBookById(int id);
        Book GetBookByAuthor(String name);

        int AddBook(Book book);
        int UpdateBook(Book book);
        int DeleteBook(int id);

    }
}

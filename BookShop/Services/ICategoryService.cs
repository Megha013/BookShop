using BookShop.Models;

namespace BookShop.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();

        Category GetCategoryById(int id);
        int AddCategory(Category cat);
        int UpdateCategory(Category cat);
        int DeleteCategory(int id);
    }
}

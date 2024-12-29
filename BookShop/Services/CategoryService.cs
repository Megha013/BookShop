using BookShop.Models;
using BookShop.Repository;

namespace BookShop.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository repo;

        public CategoryService(ICategoryRepository repo)
        {
            this.repo = repo;
        }

        public int AddCategory(Category cat)
        {
            return repo.AddCategory(cat);
        }

        public int DeleteCategory(int id)
        {
            return repo.DeleteCategory(id);
        }

        public IEnumerable<Category> GetCategories()
        {
            return repo.GetCategories();
        }

        public Category GetCategoryById(int id)
        {
            return repo.GetCategoryById(id);
        }

        public int UpdateCategory(Category cat)
        {
            return repo.UpdateCategory(cat);
        }
    }
}

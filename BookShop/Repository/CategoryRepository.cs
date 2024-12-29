using BookShop.Data;
using BookShop.Models;

namespace BookShop.Repository
{
    public class CategoryRepository:ICategoryRepository
    {
            private readonly ApplicationDbContext db;

            public CategoryRepository(ApplicationDbContext db)
            {
                this.db = db;
            }

            public int AddCategory(Category cat)
            {
                int result = 0;
                db.Categories?.Add(cat);
                result = db.SaveChanges();
                return result;
            }

            public int DeleteCategory(int id)
            {
                int result = 0;
                var c = db.Categories.Where(x => x.CategoryId == id).SingleOrDefault();
                if (c != null)
                {
                    db.Categories.Remove(c);
                    result = db.SaveChanges();
                }
                return result;
            }

            public IEnumerable<Category> GetCategories()
            {
                return db.Categories.ToList();
            }

            public Category GetCategoryById(int id)
            {
                return db.Categories.Where(x => x.CategoryId == id).SingleOrDefault();
            }

            public int UpdateCategory(Category cat)
            {
                int result = 0;
                var c = db.Categories.Where(x => x.CategoryId == cat.CategoryId).SingleOrDefault();
                if (c != null)
                {
                    c.CategoryName = cat.CategoryName;
                    result = db.SaveChanges();
                }
                return result;
            }
        
    }
}

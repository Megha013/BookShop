using BookShop.Data;
using BookShop.Models;

namespace BookShop.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext db;

        public UserRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public User ValidateUser(string email, string password)
        {

            return db.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();

        }
        public int RegisterUser(User user)
        {
            int result = 0;
            db.Users.Add(user);
            result = db.SaveChanges();
            return result;
        }
    }

}

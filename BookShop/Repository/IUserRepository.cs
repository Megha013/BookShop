using BookShop.Models;

namespace BookShop.Repository
{
    public interface IUserRepository
    {
        public User ValidateUser(string email, string password);
        public int RegisterUser(User user);
    }
}

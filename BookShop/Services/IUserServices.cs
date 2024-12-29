using BookShop.Models;

namespace BookShop.Services
{
    public interface IUserServices
    {
        public User ValidateUser(string email, string password);
        public int RegisterUser(User user);
      
    }
}

using BookShop.Models;
using BookShop.Repository;

namespace BookShop.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository repo;

        public UserServices(IUserRepository repo)
        {
            this.repo = repo;
        }

        public int RegisterUser(User user)
        {
            return repo.RegisterUser(user);
        }

        public User ValidateUser(string email, string password)
        {
            return repo.ValidateUser(email, password);
        }
    }
}

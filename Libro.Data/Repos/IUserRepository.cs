using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public interface IUserRepository
    {
        void RegisterUser(User user);
        public bool IsUsernameTaken(string username);
        public User AuthenticateUser(string username, string password);
        public User GetUserById(int id);
    }
}

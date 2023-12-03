using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public interface IUserRepository
    {
        User Login(string username, string password);
        void RegisterUser(string username, string password);
        public bool IsUsernameTaken(string username);
        public User AuthenticateUser(string username, string password);
        public User GetUserById(int id);
    }
}

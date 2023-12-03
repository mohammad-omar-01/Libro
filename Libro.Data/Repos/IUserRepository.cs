using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public interface IUserRepository
    {
        void AssignUserRole(User user, UserRole role);
        User Login(string username, string password);
        void RegisterUser(string username, string password);
        public bool IsUsernameTaken(string username);
    }
}

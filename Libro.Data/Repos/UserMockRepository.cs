using Libro.Data.DTOs;
using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public class UserMockRepository : IUserRepository
    {
        private readonly List<User> users = new List<User>
        {
            new User
            {
                ID = 1,
                Username = "MoOmar",
                Password = "omar123",
                Role = UserRole.Patron
            },
            new User
            {
                ID = 2,
                Username = "AhmadOmar",
                Password = "ahmad123",
                Role = UserRole.Admin
            },
        };

        public UserMockRepository() { }

        public User GetUserById(int id)
        {
            return users.FirstOrDefault(user => user.ID == id);
        }

        public void RegisterUser(User user)
        {
            if (
                string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password)
            )
            {
                throw new ArgumentException("Username and password cannot be empty.");
            }

            if (users.Any(u => u.Username == user.Username))
            {
                throw new InvalidOperationException("Username already exists.");
            }

            User newUser = new User
            {
                ID = GenerateNewUserId(),
                Username = user.Username,
                Password = user.Password,
                Role = user.Role
            };

            users.Add(newUser);
        }

        private int GenerateNewUserId()
        {
            return users.Count + 1;
        }

        public bool IsUsernameTaken(string username)
        {
            return users.Any(u => u.Username == username);
        }

        public User AuthenticateUser(string username, string password)
        {
            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            return user;
        }

        public PatronProfileDTO GetPatronProfileById(int userId)
        {
            throw new NotImplementedException();
        }
    }
}

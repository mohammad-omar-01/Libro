using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public class UserMockRepository : IUserRepository
    {
        private readonly List<User> users;

        public UserMockRepository()
        {
            users = new List<User>();
        }

        public void RegisterUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Username and password cannot be empty.");
            }

            if (users.Any(u => u.Username == username))
            {
                throw new InvalidOperationException("Username already exists.");
            }

            User newUser = new User
            {
                ID = GenerateNewUserId(),
                Username = username,
                Password = password,
                Role = UserRole.Patron
            };

            users.Add(newUser);
        }

        public User Login(string username, string password)
        {
            User user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }

        public void AssignUserRole(User user, UserRole role)
        {
            if (user.Role != UserRole.Admin)
            {
                throw new InvalidOperationException("Only administrators can assign roles.");
            }

            user.Role = role;
        }

        private int GenerateNewUserId()
        {
            return users.Count + 1;
        }

        public bool IsUsernameTaken(string username)
        {
            return users.Any(u => u.Username == username);
        }
    }
}

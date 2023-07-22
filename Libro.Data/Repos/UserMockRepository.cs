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
            // Find the user by username and password (replace this with your actual user lookup logic)
            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            return user;
        }
    }
}

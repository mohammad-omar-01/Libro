using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Data.Repos
{
    using Libro.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserMockRepository : IUserMockRepository
    {
        private readonly List<User> users;

        public UserMockRepository()
        {
            users = new List<User>();
        }

        public void RegisterUser(string username, string password, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Username and password cannot be empty.");
            }

            if (users.Any(u => u.Username == username))
            {
                throw new InvalidOperationException("Username already exists.");
            }

            // In a real application, store the password securely using hashing and salting
            User newUser = new User
            {
                ID = GenerateNewUserId(),
                Username = username,
                Password = password,
                Role = role
            };

            users.Add(newUser);
        }

        // Log in user and return the user object if successful, otherwise return null
        public User Login(string username, string password)
        {
            User user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }

        // Assign role to a user (only administrators can do this)
        public void AssignUserRole(User user, UserRole role)
        {
            if (user.Role != UserRole.Admin)
            {
                throw new InvalidOperationException("Only administrators can assign roles.");
            }

            user.Role = role;
        }

        // Helper method to generate a new unique user ID (replace this with your ID generation logic)
        private int GenerateNewUserId()
        {
            return users.Count + 1;
        }
    }
}

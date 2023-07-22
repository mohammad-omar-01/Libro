using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly LibroDbContext _dbContext;

        public UserRepository(LibroDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User? AuthenticateUser(string username, string password)
        {
            return _dbContext.Users.SingleOrDefault(
                u => u.Username == username && u.Password == password
            );
        }

        public void RegisterUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public bool IsUsernameTaken(string username)
        {
            return _dbContext.Users.Any(u => u.Username == username);
        }

        public User GetUserById(int id)
        {
            return _dbContext.Users.Find(id);
        }
    }
}

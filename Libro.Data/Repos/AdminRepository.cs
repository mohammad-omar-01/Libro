using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public class AdminRepository : IAdminRepository
    {
        private readonly LibroDbContext _dbContext;

        public AdminRepository(LibroDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User AssignUserRole(User user, UserRole role)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.ID == user.ID);
            existingUser.Role = role;
            _dbContext.SaveChanges();
            return user;
        }
    }
}

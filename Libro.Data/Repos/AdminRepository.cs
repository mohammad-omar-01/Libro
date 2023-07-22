using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public class AdminRepository : IAdminRepository
    {
        public AdminRepository() { }

        public User AssignUserRole(User user, UserRole role)
        {
            user.Role = role;
            return user;
        }
    }
}

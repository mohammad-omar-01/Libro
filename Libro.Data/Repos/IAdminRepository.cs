using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public interface IAdminRepository
    {
        User AssignUserRole(User user, UserRole role);
    }
}

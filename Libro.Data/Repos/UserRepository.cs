using Libro.Data.DTOs;
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

        public User AuthenticateUser(string username, string password)
        {
            return _dbContext.Users.FirstOrDefault(
                u => u.Username.Equals(username) && u.Password.Equals(password)
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

        public PatronProfileDTO GetPatronProfileById(int userId)
        {
            var user = GetUserById(userId);
            var transactions = _dbContext.Transactions.Where(a => a.PatronId == userId).ToList();
            List<BorrowingHistoryDTO> borrowings = new List<BorrowingHistoryDTO>();
            foreach (var transaction in transactions)
            {
                borrowings.Add(
                    new BorrowingHistoryDTO
                    {
                        BookCopyID = transaction.BookCopyId,
                        BookBorrowDate = transaction.Borrowdate,
                        BookReturnDate = transaction.ReturnDate,
                        BookTitle = _dbContext.Books
                            .FirstOrDefault(
                                a =>
                                    a.BookID
                                    == (
                                        _dbContext.BookCopies
                                            .FirstOrDefault(a => a.CopyId == transaction.BookCopyId)
                                            .BookId
                                    )
                            )
                            .Title
                    }
                );
            }
            PatronProfileDTO patronProfileDTO = new PatronProfileDTO
            {
                Name = user.Name,
                PatronId = userId,
                BorrowingHistory = borrowings
            };

            return patronProfileDTO;
        }
    }
}

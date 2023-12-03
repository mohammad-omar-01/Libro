using AutoMapper;
using Libro.Data.DTOs;
using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly LibroDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(LibroDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
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
                        BookTransactionId = transaction.TransactionId,
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

        public void UpdatePatronProfile(PatronProfileUpdateDTO patronProfileUpdateDTO)
        {
            var borrowings = GetPatronProfileById(patronProfileUpdateDTO.PatronId).BorrowingHistory;
            foreach (var borrow in borrowings)
            {
                patronProfileUpdateDTO.BorrowingHistory.ForEach(
                    b => b.BookTransactionId = borrow.BookTransactionId
                );
            }

            var transactions = _dbContext.Transactions
                .Where(a => a.PatronId == patronProfileUpdateDTO.PatronId)
                .ToList();
            foreach (var transaction in transactions)
            {
                foreach (var borrowing in patronProfileUpdateDTO.BorrowingHistory)
                {
                    if (borrowing.BookTransactionId == transaction.TransactionId)
                    {
                        transaction.Borrowdate = borrowing.BookBorrowDate.Value;
                        transaction.ReturnDate = borrowing.BookReturnDate;
                        transaction.BookCopyId = borrowing.BookCopyID;
                    }
                }
            }
            _dbContext.SaveChanges();
        }
    }
}

using AutoMapper;
using Libro.Data.DTOs;
using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly LibroDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAesEncryptionUtility _aesEncryptionUtility;

        public UserRepository(
            LibroDbContext dbContext,
            IMapper mapper,
            IAesEncryptionUtility aesEncryptionUtility
        )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _aesEncryptionUtility = aesEncryptionUtility;
        }

        public User AuthenticateUser(string username, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username.Equals(username));
            if (user == null)
            {
                return null;
            }
            if (_aesEncryptionUtility.Encrypt(password) == user.Password)
            {
                return user;
            }
            return null;
        }

        public void RegisterUser(User user)
        {
            user.Password = _aesEncryptionUtility.Encrypt(user.Password);
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

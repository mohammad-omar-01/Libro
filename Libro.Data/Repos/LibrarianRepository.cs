using Libro.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Data.Repos
{
    public class LibrarianRepository : ILibrarian
    {
        private readonly LibroDbContext _dbContext;

        public LibrarianRepository(LibroDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AcceptReturnedBook(int transactionId)
        {
            var transaction = _dbContext.Transactions.FirstOrDefault(
                t => t.TransactionId == transactionId
            );

            if (transaction == null)
            {
                throw new ArgumentException("Transaction not found.");
            }

            transaction.ReturnDate = DateTime.Now;

            var bookCopy = _dbContext.BookCopies.FirstOrDefault(
                bc => bc.CopyId == transaction.BookCopyId
            );

            if (bookCopy != null)
            {
                bookCopy.IsAvailable = true;
            }
            _dbContext.SaveChanges();
        }

        public List<Transction> GetOverdueBooks()
        {
            var currentDate = DateTime.Now;

            return _dbContext.Transactions
                .Where(t => t.DueDate < currentDate && t.ReturnDate == null)
                .ToList();
        }

        public void AddTransaction(Transction transaction)
        {
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
        }

        public void DeleteTransaction(int transactionID)
        {
            _dbContext.Transactions.Remove(
                _dbContext.Transactions.FirstOrDefault(T => T.TransactionId == transactionID)
            );
            _dbContext.SaveChanges();
        }

        public void UpdateTransaction(Transction transaction)
        {
            var existingTransaction = _dbContext.Transactions.FirstOrDefault(
                t => t.TransactionId == transaction.TransactionId
            );

            if (existingTransaction == null)
            {
                throw new ArgumentException("Transaction not found.");
            }

            // Update the transaction properties with the new values
            existingTransaction.BookCopyId = transaction.BookCopyId;
            existingTransaction.PatronId = transaction.PatronId;
            existingTransaction.Borrowdate = transaction.Borrowdate;
            existingTransaction.ReturnDate = transaction.ReturnDate;
            _dbContext.SaveChanges();
        }
    }
}

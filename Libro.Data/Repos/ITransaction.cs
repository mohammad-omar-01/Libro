using Libro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Libro.Data.Repos
{
    public interface ITransaction
    {
        void AddTransaction(Transction transaction);
        void DeleteTransaction(int transactionID);
        void UpdateTransaction(Transction transaction);
        public void AcceptReturnedBook(int transactionId);
        public List<Transction> GetOverdueBooks();
    }
}

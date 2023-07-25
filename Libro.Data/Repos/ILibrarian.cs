using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public interface ILibrarian
    {
        void AddTransaction(Transction transaction);
        void DeleteTransaction(int transactionID);
        void UpdateTransaction(Transction transaction);
        public void AcceptReturnedBook(int transactionId);
    }
}

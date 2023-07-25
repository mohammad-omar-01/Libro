using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public interface ITransaction
    {
        void AddTransaction(Transction transaction);
        void DeleteTransaction(int transactionID);
        void UpdateTransaction(Transction transaction);
    }
}

using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public interface IBookCopy
    {
        bool AnyCopyAvalaible(int bookID);
        BookCopy ReserveAcopy(int bookId);
    }
}

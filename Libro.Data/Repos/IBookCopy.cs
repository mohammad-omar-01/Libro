using Libro.Data.DTOs;
using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public interface IBookCopy
    {
        bool AnyCopyAvalaible(int bookID);
        BookReservationDTO ReserveAcopy(int bookId);
    }
}

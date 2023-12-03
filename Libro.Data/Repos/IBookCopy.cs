using Libro.Data.DTOs;
using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public interface IBookCopy
    {
        bool AnyCopyAvalaible(int bookID);
        BookCopy GetBookCopy(int copyId);
        BookReservationDTO ReserveAcopy(int bookId, int patronId);
    }
}

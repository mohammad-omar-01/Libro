using Libro.Data.DTOs;
using Libro.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Libro.Data.Repos
{
    public class BookCopyRepisotory : IBookCopy
    {
        private readonly LibroDbContext _dbContext;

        public BookCopyRepisotory(LibroDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public bool AnyCopyAvalaible(int bookID)
        {
            return _dbContext.BookCopies
                .Where(b => b.BookId == bookID)
                .Any(bookCopy => bookCopy.IsAvailable == true);
        }

        public BookCopy GetBookCopy(int copyId)
        {
            return _dbContext.BookCopies.FirstOrDefault(copy => copy.CopyId == copyId);
        }

        public BookReservationDTO ReserveAcopy(int BookId, int patronId)
        {
            var rertivedBook = _dbContext.Books
                .Include(b => b.Copies)
                .FirstOrDefault(book => book.BookID == BookId);
            if (rertivedBook == null)
            {
                return null;
            }
            if (!rertivedBook.AnyCopiesAvailable())
            {
                return null;
            }

            var copy = rertivedBook.Copies.FirstOrDefault(bookCopy => bookCopy.IsAvailable == true);
            copy.IsAvailable = false;
            Reservation reservation = new Reservation(copy.CopyId, patronId, DateTime.Now);
            _dbContext.Reservations.Add(reservation);
            _dbContext.SaveChangesAsync();

            BookReservationDTO copyDto = new BookReservationDTO(
                copy.CopyId,
                copy.BookId,
                rertivedBook.Title
            );

            return copyDto;
        }
    }
}

using Libro.Data.DTOs;
using Libro.Data.Models;
using Microsoft.EntityFrameworkCore;

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

        public BookReservationDTO ReserveAcopy(int BookId)
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

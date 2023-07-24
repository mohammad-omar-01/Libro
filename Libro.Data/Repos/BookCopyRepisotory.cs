using Libro.Data.Models;

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

        public BookCopy ReserveAcopy(int BookId)
        {
            var copy = _dbContext.BookCopies.FirstOrDefault(
                copy => copy.BookId == BookId && copy.IsAvailable == true
            );
            if (copy == null)
            {
                return null;
            }
            copy.IsAvailable = false;
            _dbContext.SaveChangesAsync();
            return copy;
        }
    }
}

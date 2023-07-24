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
    }
}

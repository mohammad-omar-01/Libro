using Libro.Data.Models;

namespace Libro.Data.Repos
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibroDbContext _dbContext;

        public AuthorRepository(LibroDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddBookToAuthor(int bookId, int authorId)
        {
            var author = _dbContext.Authors.FirstOrDefault(a => a.AuthorID == authorId);
            var book = _dbContext.Books.FirstOrDefault(a => a.BookID == bookId);
            if (book != null)
            {
                author.Books.Add(book);
                _dbContext.SaveChanges();
            }
        }

        public void AddAuthor(Author author)
        {
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
        }

        public void DeleteAuthor(int authorId)
        {
            _dbContext.Authors.Remove(GetAuthorById(authorId));
            _dbContext.SaveChanges();
        }

        public List<Author> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public Author GetAuthorById(int authorId)
        {
            return _dbContext.Authors.FirstOrDefault(author => author.AuthorID == authorId);
        }

        public void UpdateAuthor(Author updatedAuthor)
        {
            var existingAuthor = _dbContext.Authors.Find(updatedAuthor.AuthorID);

            if (existingAuthor == null)
            {
                throw new ArgumentException("Author not found");
            }

            existingAuthor.Name = updatedAuthor.Name;

            _dbContext.SaveChanges();
        }
    }
}

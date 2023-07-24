using Libro.Data.Models;

public interface IBookRepository
{
    void AddBook(Book book);
    void DeleteBook(int bookId);
    List<Book> GetAllBooks(int pageNumber, int pageSize);
    List<Book> GetAllBooks();
    Book GetBookById(int bookId);
    void UpdateBook(Book updatedBook);

    bool AddAuthorToBook(int bookId, int authorId);
}

using Libro.Data.Models;

public interface IBookRepository
{
    void AddBook(Book book);
    void DeleteBook(int bookId);
    List<Book> GetAllBooks();
    Book GetBookById(int bookId);
    void UpdateBook(Book updatedBook);
    BookCopy ReserveCopy(Book book);
    void AddBookCopy(int bookId, bool isAvailable);
}

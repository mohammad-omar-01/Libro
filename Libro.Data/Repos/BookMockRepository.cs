using Libro.Data.Models;
using System.Security.Cryptography;

public class BookMockRepository : IBookRepository
{
    private List<Book> books;
    private int bookIdCounter;
    private Random random;

    public BookMockRepository()
    {
        books = new List<Book>();
        bookIdCounter = 1;
        random = new Random();

        AddSampleData();
    }

    private void AddSampleData()
    {
        Author author1 = new Author { AuthorID = 1, Name = "Jane Austen" };
        Author author2 = new Author { AuthorID = 2, Name = "Charles Dickens" };

        Book book1 = new Book
        {
            BookID = bookIdCounter++,
            Title = "Pride and Prejudice",
            Genre = "Romance",
            PublicationDate = new DateTime(1813, 1, 28),
            AvailabilityStatus = true
        };
        book1.Authors.Add(author1);

        Book book2 = new Book
        {
            BookID = bookIdCounter++,
            Title = "Great Expectations",
            Genre = "Fiction",
            PublicationDate = new DateTime(1861, 1, 14),
            AvailabilityStatus = true
        };
        book2.Authors.Add(author2);

        books.Add(book1);
        books.Add(book2);
    }

    public void AddBook(Book book)
    {
        book.BookID = bookIdCounter++;
        books.Add(book);
    }

    public void UpdateBook(Book updatedBook)
    {
        Book existingBook = GetBookById(updatedBook.BookID);
        if (existingBook != null)
        {
            existingBook.Title = updatedBook.Title;
            existingBook.Genre = updatedBook.Genre;
            existingBook.PublicationDate = updatedBook.PublicationDate;
            existingBook.AvailabilityStatus = updatedBook.AvailabilityStatus;
            existingBook.Authors = updatedBook.Authors;
        }
    }

    public void DeleteBook(int bookId)
    {
        Book bookToRemove = GetBookById(bookId);
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
        }
    }

    public Book GetBookById(int bookId)
    {
        return books.FirstOrDefault(b => b.BookID == bookId);
    }

    public void AddBookCopy(int bookId, bool isAvailable)
    {
        Book book = books.FirstOrDefault(b => b.BookID == bookId);

        if (book == null)
        {
            throw new ArgumentException("Book with specified ID not found.");
        }

        int copyId = GenerateNewCopyId(bookId);
        BookCopy newCopy = new BookCopy(copyId, bookId, isAvailable);
        book.Copies.Add(newCopy);
    }

    private int GenerateNewCopyId(int bookId)
    {
        return bookId + (random.Next(20));
        ;
    }

    public List<Book> GetAllBooks()
    {
        return books;
    }

    public BookCopy ReserveCopy(Book book)
    {
        var copy = book.Copies.FirstOrDefault(copy => copy.IsAvailable);

        if (copy != null)
            book.Copies.ElementAt(book.Copies.IndexOf(copy)).IsAvailable = false;

        return copy;
    }
}

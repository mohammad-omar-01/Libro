using Libro.Data.Models;

public class BookMockRepositoryTests
{
    private BookMockRepository bookRepository;

    public BookMockRepositoryTests()
    {
        bookRepository = new BookMockRepository();
    }

    [Fact]
    public void AddBook_ShouldIncrementBookIdCounter()
    {
        int initialCounter = bookRepository.GetAllBooks().Count;
        Book newBook = new Book
        {
            Title = "Test Book",
            Genre = "Test Genre",
            PublicationDate = DateTime.Now,
            AvailabilityStatus = true
        };

        bookRepository.AddBook(newBook);

        int finalCounter = bookRepository.GetAllBooks().Count;
        Assert.Equal(initialCounter + 1, finalCounter);
    }

    [Fact]
    public void UpdateBook_ShouldUpdateBookProperties()
    {
        Book existingBook = bookRepository.GetBookById(1);
        if (existingBook != null)
        {
            Book updatedBook = new Book
            {
                BookID = existingBook.BookID,
                Title = "Updated Title",
                Genre = "Updated Genre",
                PublicationDate = existingBook.PublicationDate,
                AvailabilityStatus = existingBook.AvailabilityStatus
            };

            bookRepository.UpdateBook(updatedBook);

            Book updatedBookFromRepository = bookRepository.GetBookById(existingBook.BookID);
            Assert.Equal("Updated Title", updatedBookFromRepository.Title);
            Assert.Equal("Updated Genre", updatedBookFromRepository.Genre);
        }
    }

    [Fact]
    public void DeleteBook_ShouldRemoveBookFromRepository()
    {
        int initialCounter = bookRepository.GetAllBooks().Count;
        int bookIdToDelete = 1;

        bookRepository.DeleteBook(bookIdToDelete);

        int finalCounter = bookRepository.GetAllBooks().Count;
        Assert.Equal(initialCounter - 1, finalCounter);
        Assert.Null(bookRepository.GetBookById(bookIdToDelete));
    }
}

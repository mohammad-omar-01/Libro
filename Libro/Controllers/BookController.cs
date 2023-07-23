using Libro.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            List<Book> books = _bookRepository.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("available")]
        public IActionResult GetAvailableBooks()
        {
            List<Book> availableBooks = _bookRepository
                .GetAllBooks()
                .Where(b => b.AnyCopiesAvailable())
                .ToList();
            if (availableBooks.Count > 0)
            {
                return Ok(availableBooks);
            }
            else
                return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Book book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            else

                return Ok(book);
        }

        [HttpPut("{bookId}/authors/{authorId}")]
        public IActionResult AddAuthorToBook(int bookId, int authorId)
        {
            var book = _bookRepository.GetBookById(bookId);
            var author = _authorRepository.GetAuthorById(authorId);

            if (book == null || author == null)
            {
                return NotFound();
            }

            _bookRepository.AddAuthorToBook(book.BookID, author.AuthorID);

            return Ok();
        }

        [HttpPost("{bookId}/reserve")]
        public IActionResult ReserveBook(int bookId)
        {
            Book book = _bookRepository.GetBookById(bookId);
            if (book == null)
            {
                return NotFound();
            }

            if (!book.AnyCopiesAvailable())
            {
                return BadRequest("The book is not available for reservation.");
            }

            var copyToReserve = book.Copies.FirstOrDefault(copy => copy.IsAvailable);
            book.AvailabilityStatus = false;

            return Ok("Book reserved successfully.");
        }
    }
}

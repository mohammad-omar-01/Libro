using Libro.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
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
                .Where(b => b.AvailabilityStatus)
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

        [HttpPost("{bookId}/reserve")]
        public IActionResult ReserveBook(int bookId)
        {
            Book book = _bookRepository.GetBookById(bookId);
            if (book == null)
            {
                return NotFound();
            }

            if (!book.AvailabilityStatus)
            {
                return BadRequest("The book is not available for reservation.");
            }

            if (book.IsReserved)
            {
                return BadRequest("The book is already reserved.");
            }

            book.IsReserved = true;

            return Ok("Book reserved successfully.");
        }
    }
}

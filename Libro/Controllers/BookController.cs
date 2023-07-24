using AutoMapper;
using Libro.Data.DTOs;
using Libro.Data.Models;
using Libro.Data.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookCopy _bookCopyRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public BookController(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IBookCopy bookCopy,
            IMapper mapper
        )
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _bookCopyRepository = bookCopy;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1)
        {
            List<Book> books = _bookRepository.GetAllBooks(pageNumber, pageSize);
            var dtoBooks = _mapper.Map<List<BookSearchDTO>>(books);
            return Ok(dtoBooks);
        }

        [HttpGet("available")]
        public IActionResult GetAvailableBooks()
        {
            List<Book> availableBooks = _bookRepository
                .GetAllBooks()
                .Where(book => _bookCopyRepository.AnyCopyAvalaible(book.BookID))
                .ToList();
            if (availableBooks.Count > 0)
            {
                var availableBookDto = _mapper.Map<BookSearchDTO>(availableBooks);
                return Ok(availableBookDto);
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
            var bookDto = _mapper.Map<BookSearchDTO>(book);

            return Ok(bookDto);
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

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

            return Ok(availableBooks);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value) { }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}

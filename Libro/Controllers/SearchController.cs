using AutoMapper;
using Libro.Data.DTOs;
using Libro.Data.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly IBookRepository bookRepository;
    private readonly IMapper _mapper;

    public SearchController(IBookRepository bookRepository, IMapper mapper)
    {
        this.bookRepository = bookRepository;
        this._mapper = mapper;
    }

    [HttpGet("books")]
    public IActionResult SearchBooks(string query = null)
    {
        List<Book> books = bookRepository.GetAllBooks();

        if (!string.IsNullOrWhiteSpace(query))
        {
            books = books
                .Where(
                    b =>
                        b.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
                        || b.Authors.Any(
                            a => a.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
                        )
                        || b.Genre.Contains(query, StringComparison.OrdinalIgnoreCase)
                )
                .ToList();

            return Ok(books);
        }
        List<BookDTO> bookDTOs = _mapper.Map<List<BookDTO>>(books);

        return Ok(bookDTOs);
    }
}

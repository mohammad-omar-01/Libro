using AutoMapper;
using Libro.Data.DTOs;
using Libro.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Libro.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Policy = "MustNotBePatron")]
        public ActionResult<AuthorDTO> AddAuthor([FromBody] AuthorDTO authorDTO)
        {
            var authorToAdd = _mapper.Map<Author>(authorDTO);
            _authorRepository.AddAuthor(authorToAdd);
            var createdAuthorDTO = _mapper.Map<AuthorDTO>(authorToAdd);
            return Created("Author created successfully", createdAuthorDTO);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "MustNotBePatron")]
        public IActionResult EditAuthor(int id, [FromBody] AuthorDTO authorDTO)
        {
            var author = _mapper.Map<Author>(authorDTO);
            _authorRepository.UpdateAuthor(author);

            return Ok("Author updated successfully.");
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "MustNotBePatron")]
        public IActionResult DeleteAuthor(int id)
        {
            _authorRepository.DeleteAuthor(id);

            return Ok();
        }
    }
}

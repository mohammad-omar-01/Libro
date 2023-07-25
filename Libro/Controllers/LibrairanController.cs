using AutoMapper;
using Libro.Data.DTOs;
using Libro.Data.Models;
using Libro.Data.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrairanController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookCopy _bookCopyRepository;
        private readonly IReservation _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILibrarian _librarianRepository;

        public LibrairanController(
            IBookRepository bookRepository,
            IUserRepository userRepository,
            IBookCopy bookCopy,
            IMapper mapper,
            IReservation reservation,
            ILibrarian transaction
        )
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _bookCopyRepository = bookCopy;
            _mapper = mapper;
            _reservationRepository = reservation;
            _librarianRepository = transaction;
        }

        [Authorize(Policy = "MustBeLibrarian")]
        [HttpPost("checkout")]
        public IActionResult CheckoutBook([FromBody] BookCheckoutDTO checkoutDTO)
        {
            var patron = _userRepository.GetUserById(checkoutDTO.PatronId);
            var bookCopy = _bookCopyRepository.GetBookCopy(checkoutDTO.BookCopyId);

            if (patron == null || bookCopy == null)
            {
                return NotFound();
            }
            if (bookCopy.IsAvailable == false)
            {
                var reservationForPatron = _reservationRepository.GetReservationsByPatronId(
                    checkoutDTO.PatronId
                );
                if (reservationForPatron.Any(b => b.BookCopyId == checkoutDTO.BookCopyId))
                {
                    DateTime checkoutDate = DateTime.Now;
                    DateTime? returnDate = null;

                    Transction transaction = new Transction(
                        bookCopy.BookId,
                        patron.ID,
                        checkoutDate,
                        returnDate
                    );
                    transaction.DueDate = checkoutDate.AddDays(14);
                    _librarianRepository.AddTransaction(transaction);
                    return Created("Book checked out successfully!\n", transaction);
                }
                return BadRequest("Book is Reserved!");
            }

            if (bookCopy.IsAvailable)
            {
                DateTime checkoutDate = DateTime.Now;
                DateTime returnDate = new DateTime();
                Transction transaction = new Transction(
                    bookCopy.BookId,
                    patron.ID,
                    checkoutDate,
                    returnDate
                );
                transaction.DueDate = DateTime.Now.AddDays(14);
                _librarianRepository.AddTransaction(transaction);
                return Ok("Book checked out successfully!");
            }
            return BadRequest("Book is already checked out!");
        }

        [Authorize(Policy = "MustBeLibrarian")]
        [HttpPost("AcceptReturnedBook/{transactionId}")]
        public IActionResult AcceptReturnedBook(int transactionId)
        {
            try
            {
                _librarianRepository.AcceptReturnedBook(transactionId);
                return Ok("Book returned successfully and made available for other patrons.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [Authorize(Policy = "MustBeLibrarian")]
        [HttpGet("GetOverdueBooks")]
        public ActionResult<List<Transction>> GetOverdueBooks()
        {
            var currentDate = DateTime.Now;

            var overdueBooks = _librarianRepository.GetOverdueBooks();

            return overdueBooks;
        }
    }
}

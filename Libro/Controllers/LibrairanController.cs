using AutoMapper;
using Libro.Data.DTOs;
using Libro.Data.Models;
using Libro.Data.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly ITransaction _transactionRepository;

        public LibrairanController(
            IBookRepository bookRepository,
            IUserRepository userRepository,
            IBookCopy bookCopy,
            IMapper mapper,
            IReservation reservation,
            ITransaction transaction
        )
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _bookCopyRepository = bookCopy;
            _mapper = mapper;
            _reservationRepository = reservation;
            _transactionRepository = transaction;
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
                    DateTime returnDate = DateTime.Now.AddMonths(1);

                    Transction transaction = new Transction(
                        bookCopy.BookId,
                        patron.ID,
                        checkoutDate,
                        returnDate
                    );
                    _transactionRepository.AddTransaction(transaction);
                    return Created("Book checked out successfully!\n", transaction);
                }
                return BadRequest("Book is Reserved!");
            }

            if (bookCopy.IsAvailable)
            {
                DateTime checkoutDate = DateTime.Now;
                DateTime returnDate = DateTime.Now.AddMonths(1);
                Transction transaction = new Transction(
                    bookCopy.BookId,
                    patron.ID,
                    checkoutDate,
                    returnDate
                );
                _transactionRepository.AddTransaction(transaction);
                return Ok("Book checked out successfully!");
            }
            return BadRequest("Book is already checked out!");
        }
    }
}

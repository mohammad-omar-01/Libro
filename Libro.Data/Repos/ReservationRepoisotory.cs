using Libro.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Data.Repos
{
    public class ReservationRepoisotory : IReservation
    {
        private readonly LibroDbContext _dbContext;

        public ReservationRepoisotory(LibroDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Reservation> GetReservationsByPatronId(int patronId)
        {
            var reservations = _dbContext.Reservations.Where(r => r.PatronId == patronId).ToList();

            return reservations;
        }
    }
}

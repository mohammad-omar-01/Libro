using Libro.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Data.Repos
{
    public interface IReservation
    {
        List<Reservation> GetReservationsByPatronId(int patronId);
    }
}

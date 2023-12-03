using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libro.Data.Models
{
    public class Reservation
    {
        public Reservation() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationId { get; set; }

        [ForeignKey("BookCopy")]
        public int BookCopyId { get; set; }

        [ForeignKey("User")]
        public int PatronId { get; set; }
        public DateTime ReservationDate { get; set; }

        public Reservation(int copyId, int patronId, DateTime dateTime)
        {
            this.ReservationDate = dateTime;
            this.BookCopyId = copyId;
            this.PatronId = patronId;
        }
    }
}

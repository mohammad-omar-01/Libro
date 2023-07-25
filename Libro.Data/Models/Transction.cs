using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libro.Data.Models
{
    public class Transction
    {
        public Transction() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [ForeignKey("BookCopy")]
        public int BookCopyId { get; set; }

        [ForeignKey("User")]
        public int PatronId { get; set; }

        public DateTime Borrowdate { get; set; }
        public DateTime ReturnDate { get; set; }

        public Transction(int bookCopyId, int patronId, DateTime borrowdate, DateTime returnDate)
        {
            BookCopyId = bookCopyId;
            PatronId = patronId;
            Borrowdate = borrowdate;
            ReturnDate = returnDate;
        }
    }
}

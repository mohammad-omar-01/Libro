using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Data.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        [ForeignKey("BookCopy")]
        public int BookCopyId { get; set; }

        [ForeignKey("User")]
        public int PatronId { get; set; }

        public DateTime Borrowdate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}

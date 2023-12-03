using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Data.DTOs
{
    public class TransactionDTO
    {
        public int BookCopyID { get; set; }
        public DateTime? BookBorrowDate { get; set; }
        public DateTime? BookReturnDate { get; set; }
        public int BookTransactionId { get; internal set; }
    }
}

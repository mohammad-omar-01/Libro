using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Data.DTOs
{
    public class PatronProfileDTO
    {
        public int PatronId { get; set; }
        public string Name { get; set; }
        public List<BorrowingHistoryDTO> BorrowingHistory { get; set; }
    }

    public class BorrowingHistoryDTO
    {
        public int BookCopyID { get; set; }
        public string BookTitle { get; set; }
        public DateTime? BookBorrowDate { get; set; }
        public DateTime? BookReturnDate { get; set; }
    }
}

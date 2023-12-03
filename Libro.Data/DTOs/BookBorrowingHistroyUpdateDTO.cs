namespace Libro.Data.DTOs
{
    public class BookBorrowingHistroyUpdateDTO
    {
        public int BookCopyID { get; set; }
        public string BookTitle { get; set; }
        public DateTime? BookBorrowDate { get; set; }
        public DateTime? BookReturnDate { get; set; }
        public int BookTransactionId { get; internal set; }
    }
}

namespace Libro.Data.DTOs
{
    public class PatronProfileUpdateDTO
    {
        public int PatronId { get; set; }
        public string Name { get; set; }
        public List<BookBorrowingHistroyUpdateDTO> BorrowingHistory { get; set; }
    }
}

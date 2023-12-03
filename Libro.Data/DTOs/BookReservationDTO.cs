namespace Libro.Data.DTOs
{
    public class BookReservationDTO
    {
        public BookReservationDTO(int copyId, int bookId, string title)
        {
            CopyId = copyId;
            BookId = bookId;
            Title = title;
        }

        public int CopyId { get; set; }

        public int BookId { get; set; }

        public string Title { get; set; } = string.Empty;
    }
}

namespace Libro.Data.Models
{
    public class BookCopy
    {
        public int CopyId { get; set; }
        public int BookId { get; set; }
        public bool IsAvailable { get; set; }

        public BookCopy(int copyId, int bookId, bool isAvailable = true)
        {
            CopyId = copyId;
            BookId = bookId;
            IsAvailable = isAvailable;
        }
    }
}

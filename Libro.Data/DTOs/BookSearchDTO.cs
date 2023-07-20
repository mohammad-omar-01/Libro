namespace Libro.Data.DTOs
{
    public class BookDTO
    {
        public int BookID { get; set; }
        public string BookName { get; set; } = string.Empty;
        public int AuthorID { get; set; }
        public string AuthorName { get; set; } = string.Empty;
    }
}

using Libro.Data.Models;

namespace Libro.Data.DTOs
{
    public class BookSearchDTO
    {
        public int BookID { get; set; }
        public string BookName { get; set; } = string.Empty;
        public List<AuthorDTO> Authors { get; set; } = new List<AuthorDTO>();
    }
}

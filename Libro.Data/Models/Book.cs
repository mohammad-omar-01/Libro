namespace Libro.Data.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool AvailabilityStatus { get; set; }
        public bool IsReserved { get; set; }
        public List<Author> Authors { get; set; }
        public List<BookCopy> Copies { get; set; }

        public Book()
        {
            Copies = new List<BookCopy>();
            Authors = new List<Author>();
        }

        public bool AnyCopiesAvailable()
        {
            return Copies.Any(copy => copy.IsAvailable);
        }
    }
}

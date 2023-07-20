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

        public Book()
        {
            Authors = new List<Author>();
        }
    }
}

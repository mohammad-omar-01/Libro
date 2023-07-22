using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Libro.Data.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }

        [Required]
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationDate { get; set; }

        [NotMapped]
        public virtual bool AvailabilityStatus { get; set; }
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

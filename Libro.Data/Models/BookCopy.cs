using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Libro.Data.Models
{
    public class BookCopy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CopyId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public bool IsAvailable { get; set; }

        public BookCopy(int copyId = 1, int bookId = 1, bool isAvailable = true)
        {
            CopyId = copyId;
            BookId = bookId;
            IsAvailable = isAvailable;
        }
    }
}

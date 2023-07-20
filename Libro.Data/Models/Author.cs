namespace Libro.Data.Models
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }
    }
}

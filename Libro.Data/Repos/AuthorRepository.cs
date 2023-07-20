using Libro.Data.Models;

public class AuthorMockRepository
{
    private List<Author> authors;
    private int authorIdCounter;

    public AuthorMockRepository()
    {
        authors = new List<Author>();
        authorIdCounter = 1;

        AddSampleData();
    }

    private void AddSampleData()
    {
        Author author1 = new Author { AuthorID = authorIdCounter++, Name = "Jane Austen" };
        Author author2 = new Author { AuthorID = authorIdCounter++, Name = "Charles Dickens" };

        authors.Add(author1);
        authors.Add(author2);
    }

    public void AddAuthor(Author author)
    {
        author.AuthorID = authorIdCounter++;
        authors.Add(author);
    }

    public void UpdateAuthor(Author updatedAuthor)
    {
        Author existingAuthor = GetAuthorById(updatedAuthor.AuthorID);
        if (existingAuthor != null)
        {
            existingAuthor.Name = updatedAuthor.Name;
        }
    }

    public void DeleteAuthor(int authorId)
    {
        Author authorToRemove = GetAuthorById(authorId);
        if (authorToRemove != null)
        {
            authors.Remove(authorToRemove);
        }
    }

    public Author GetAuthorById(int authorId)
    {
        return authors.FirstOrDefault(a => a.AuthorID == authorId);
    }

    public List<Author> GetAllAuthors()
    {
        return authors;
    }
}

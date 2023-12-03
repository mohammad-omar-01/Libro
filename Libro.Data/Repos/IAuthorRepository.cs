using Libro.Data.Models;

public interface IAuthorRepository
{
    void AddAuthor(Author author);
    void DeleteAuthor(int authorId);
    List<Author> GetAllAuthors();
    Author GetAuthorById(int authorId);
    void UpdateAuthor(Author updatedAuthor);
}
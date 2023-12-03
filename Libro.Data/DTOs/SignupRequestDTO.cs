namespace Libro.Data.DTOs
{
    public class SignupRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}

namespace Ticket.Models.Dtos.Auth
{
    public class UserRegisterDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public required string? Email { get; set; }
        public required string? Password { get; set; }
        public string? Role { get; set; }
    }
}

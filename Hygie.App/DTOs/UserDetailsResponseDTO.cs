namespace Hygie.App.DTOs
{
    public class UserDetailsResponseDTO
    {
        public required string Id { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public IList<string>? Roles { get; set; }
    }
}

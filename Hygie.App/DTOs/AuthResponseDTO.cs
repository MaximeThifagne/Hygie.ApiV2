namespace Hygie.App.DTOs
{
    public class AuthResponseDTO
    {
        public required string UserId { get; set; }
        public required string FullName { get; set; }
        public required string Name { get; set; }
        public required string Token { get; set; }
        public IList<string>? Roles { get; set; }
    }
}

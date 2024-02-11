using Hygie.Core.Entities;

namespace Hygie.App.DTOs
{
    public class AuthResponseDTO
    {
        public required string UserId { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required bool EmailVerified { get; set; }
        public required string Name { get; set; }
        public required string Token { get; set; }
        public string? PhoneNumber { get; set; }
        public IList<string>? Roles { get; set; }
        public Adress? Adress { get; set; }
        public ProfilePicture? ProfileImage { get; set; }
    }
}

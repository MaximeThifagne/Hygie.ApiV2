namespace Hygie.App.DTOs
{
    public class PatientResponseDTO
    {
        public Int64 Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string ContactNumber { get; set; }
        public required string Address { get; set; }
    }
}

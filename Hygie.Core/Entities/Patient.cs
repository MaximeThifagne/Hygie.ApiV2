using Hygie.Core.Entities.Base;

namespace Hygie.Core.Entities
{
    public class Patient : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string ContactNumber { get; set; }
        public required string Address { get; set; }
    }
}
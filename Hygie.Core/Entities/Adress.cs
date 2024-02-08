using Hygie.Core.Entities.Base;

namespace Hygie.Core.Entities
{
    public class Adress : BaseEntity
    {
        public required string Street { get; set; }
        public required string Number { get; set; }
        public string? Complement { get; set; }
        public string? Neighborhood { get; set; }
        public required string City { get; set; }
        public required string ZipCode { get; set; }
    }
}

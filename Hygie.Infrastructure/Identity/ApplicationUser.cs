using Hygie.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hygie.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

        [ForeignKey("Address")]
        public int? ProfilePictureId { get; set; }

        public ProfilePicture? ProfilePicture { get; set; }
    }
}

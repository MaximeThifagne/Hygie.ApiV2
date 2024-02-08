using Hygie.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hygie.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

        [ForeignKey("ProfilePicture")]
        public Int64? ProfilePictureId { get; set; }

        [ForeignKey("Adress")]
        public Int64? AdrressId { get; set; }

        public ProfilePicture? ProfilePicture { get; set; }

        public Adress? Adress { get; set; }


    }
}

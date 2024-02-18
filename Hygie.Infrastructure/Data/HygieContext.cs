using Hygie.Core.Entities;
using Hygie.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hygie.Infrastructure.Data
{
    public class HygieContext : IdentityDbContext<ApplicationUser>
    {
        public HygieContext(DbContextOptions<HygieContext> options) : base(options)
        {

        }
        public DbSet<ProfilePicture> ProfilePictures { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<PatientExitDate> PatientExitDates { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
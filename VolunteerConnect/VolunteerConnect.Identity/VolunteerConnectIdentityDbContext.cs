using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VolunteerConnect.Identity.Models;

namespace VolunteerConnect.Identity
{
    public class VolunteerConnectIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public VolunteerConnectIdentityDbContext()
        {
                
        }

        public VolunteerConnectIdentityDbContext(DbContextOptions<VolunteerConnectIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
        .LogTo(Console.WriteLine)
        .EnableSensitiveDataLogging();
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Toastmasters.Web.Models;

namespace Toastmasters.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Speech> Speeches { get; set; }
    }
}

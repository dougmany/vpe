using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Toastmasters.Web.Models;

namespace Toastmasters.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         // => optionsBuilder.UseMySql(@"Server=138.68.237.189;database=toastmasters;uid=dbuser;pwd=h5$!QEgrmXWI';");
         => optionsBuilder.UseMySql(@"Server=localhost;database=toastmasters;uid=dbuser;pwd=test123;");
        // => optionsBuilder.UseMySql(@"Server=localhost;port=3336;database=toastmasters;uid=dbuser;pwd=test123;");
    }
}

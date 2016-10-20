using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WebService.Models;

namespace WebService.DataAccess
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("AppUser");
        }
    }
}
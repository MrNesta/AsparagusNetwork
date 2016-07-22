using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace AsparagusTest.Models.Identity
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("DefaultConnection") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        public DbSet<Food> Foods { get; set; }
    }
}
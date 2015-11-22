using DAL.Membership;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using DAL.Model;

namespace DAL.Persistence
{
    [DbConfigurationType(typeof(MySqlConfiguration))]
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, ApplicationRole,
        string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {

        public DbSet<Item> Items { get; set; }

        public ApplicationDbContext()
            : base("LocalConnection")
        {
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer(new MySqlInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}

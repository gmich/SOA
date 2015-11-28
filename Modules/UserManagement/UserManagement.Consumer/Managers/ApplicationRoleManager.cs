using DAL.Membership;
using DAL.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Model.Membership;

namespace UserManagement.Consumer.Managers
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {        
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store)
            : base(store)
        { }

        public static ApplicationRoleManager Create(
            IdentityFactoryOptions<ApplicationRoleManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationRoleManager(
                new ApplicationRoleStore(
                    context.Get<ApplicationDbContext>()));

            return manager;
        }
    }
}

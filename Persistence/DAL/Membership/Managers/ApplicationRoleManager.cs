using Microsoft.AspNet.Identity;

namespace DAL.Membership.Managers
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {        
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store)
            : base(store)
        { }
            
    }
}

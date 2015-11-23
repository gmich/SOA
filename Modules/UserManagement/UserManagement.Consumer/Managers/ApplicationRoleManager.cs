using Microsoft.AspNet.Identity;
using Model.Membership;

namespace UserManagement.Consumer.Managers
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {        
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store)
            : base(store)
        { }
            
    }
}

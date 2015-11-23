using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Model.Membership
{
    public class ApplicationUser
        : IdentityUser<string, ApplicationUserLogin,
        ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();            
        }

    }
}

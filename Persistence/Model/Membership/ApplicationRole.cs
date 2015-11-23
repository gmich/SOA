using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Model.Membership
{
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>
    {
        public ApplicationRole()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public ApplicationRole(string name)
            : this()
        {
            this.Name = name;
        }

        //TODO: add custom role properties
    }
}

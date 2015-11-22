using DAL.Membership.Managers;
using Microsoft.AspNet.Identity;

namespace DAL.Membership
{
    public class ApplicationUserValidator : UserValidator<ApplicationUser, string>
    {
        public ApplicationUserValidator(ApplicationUserManager manager) : base(manager)
        {
        }
            
    }
}

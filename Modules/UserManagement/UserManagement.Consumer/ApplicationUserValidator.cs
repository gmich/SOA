using Microsoft.AspNet.Identity;
using Model.Membership;
using UserManagement.Consumer.Managers;

namespace UserManagement.Consumer
{
    public class ApplicationUserValidator : UserValidator<ApplicationUser, string>
    {
        public ApplicationUserValidator(ApplicationUserManager manager) : base(manager)
        {
        }
            
    }
}

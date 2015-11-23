using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Model.Membership;

namespace UserManagement.Consumer.Managers
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager,
                                        IAuthenticationManager authenticationManager)
        : base(userManager, authenticationManager)
        {
        }
    }
}

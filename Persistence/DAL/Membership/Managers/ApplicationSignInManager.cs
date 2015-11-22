using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace DAL.Membership.Managers
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

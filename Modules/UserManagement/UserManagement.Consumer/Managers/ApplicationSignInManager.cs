﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Model.Membership;
using System.Security.Claims;
using System.Threading.Tasks;
using UserManagement.Consumer;

namespace UserManagement.Consumer.Managers
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager,
                                        IAuthenticationManager authenticationManager)
        : base(userManager, authenticationManager)
        {
        }

        public static ApplicationSignInManager Create(
            IdentityFactoryOptions<ApplicationSignInManager> options,
            IOwinContext context)
        {
           return
                new ApplicationSignInManager(
                    context.GetUserManager<ApplicationUserManager>(),
                    context.Authentication);
        }
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager, DefaultAuthenticationTypes.ApplicationCookie);
        }

    }
}

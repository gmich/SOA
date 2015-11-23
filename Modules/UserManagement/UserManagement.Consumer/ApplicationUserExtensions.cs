﻿using System.Threading.Tasks;
using Model.Membership;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using UserManagement.Consumer.Managers;

namespace UserManagement.Consumer
{
    public static class ApplicationUserExtensions
    {
        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            this ApplicationUser user,
            ApplicationUserManager manager)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace LIMSWebPortalAPIApp.Authorization
{
    public class ProjectOwnerAuthorizationHandler : 
        AuthorizationHandler<OperationAuthorizationRequirement, UserModel>
    {
        protected override Task HandleRequirementAsync(
                                      AuthorizationHandlerContext context,
                            OperationAuthorizationRequirement requirement,
                             UserModel resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            if (resource.OwnerID == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

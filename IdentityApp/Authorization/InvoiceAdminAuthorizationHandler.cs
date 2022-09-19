namespace IdentityApp.Authorization
{
    using IdentityApp.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authorization.Infrastructure;
    using System.Threading.Tasks;

    /// <summary>
    /// It handles the authorization of Manager Role
    /// </summary>
    public class InvoiceAdminAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Invoice>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Invoice invoice)
        {
            if (context.User == null || invoice == null)
                return Task.CompletedTask;

            if (context.User.IsInRole(Constants.InvoiceAdminRole))
                context.Succeed(requirement);

            return Task.CompletedTask;

        }
    }
}

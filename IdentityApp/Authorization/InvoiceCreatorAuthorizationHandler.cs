namespace IdentityApp.Authorization
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authorization.Infrastructure;
    using Microsoft.AspNetCore.Identity;
    using IdentityApp.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// It checks if the creator is legit the creator of the invoice
    /// This is the "Accountant/user". Can only create,delete and edit it's own invoice.
    /// </summary>
    public class InvoiceCreatorAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Invoice>
    {
        UserManager<IdentityUser> _userManager;

        public InvoiceCreatorAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// hey, okay here is the user that's what he wants to do. Check the requirements
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <param name="invoice"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Invoice invoice)
        {

            if (context.User == null || invoice == null)
                return Task.CompletedTask;

            // if he's not doing crud and this is not the handler is what for
            if(requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            if (invoice.CreatorId == _userManager.GetUserId(context.User))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}

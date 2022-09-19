using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityApp.Authorization;

namespace IdentityApp.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string password)
        {
            using(var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // manager
                var managerUid = await EnsureUser(serviceProvider, "manager@demo.com", password);

                await EnsureRole(serviceProvider, managerUid, Constants.InvoiceManagersRole);

                //administrator
                var adminUid = await EnsureUser(serviceProvider, "admin@demo.com", password);

                await EnsureRole(serviceProvider, adminUid, Constants.InvoiceAdminRole);
            }
        }

        /// <summary>
        /// creates a user
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="initPw"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static async Task<string> EnsureUser(
            IServiceProvider serviceProvider,
            string userName,
            string initPw)
        {
            //get serve takes usermnager type of identity
            //seeding
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            //checks if manager account exists. only 1 acc type of manager exist
            var user = await userManager.FindByNameAsync(userName);

            if(user == null)
            {
                user = new IdentityUser
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, initPw);
            }

            if (user == null)
                throw new Exception("User did not get created. Password policy problem?.");

            return user.Id;

        }

        /// <summary>
        /// Assigns a specific role to a user
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="uid"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static async Task<IdentityResult> EnsureRole(
            IServiceProvider serviceProvider, string uid, string role
            )
        {

            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            IdentityResult ir;

            if(await roleManager.RoleExistsAsync(role) == false)
            {
                ir = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if(user == null)
            {
                throw new Exception("User not existing");
            }

            //add to specific role
            ir = await userManager.AddToRoleAsync(user, role);

            return ir;
        }

    }
}

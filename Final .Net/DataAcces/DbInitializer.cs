using Core.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),

                    });
                }
            }
            if ((await userManager.FindByNameAsync("Faig")) == null)
            {
                var user = new User
                {
                    FullName = "FaigResul",
                    UserName = "Faig",
                    Email = "FaigResull@gmail.com"
                };
                var result = await userManager.CreateAsync(user, "Admin1234!");
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }
                await userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
            }
        }
    }
}

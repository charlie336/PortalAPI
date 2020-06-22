using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace LIMSWebPortalAPIApp.Data
{
    public static class SeedData
    {
        public async static Task Seed(UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);

        }
        private async static Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            if(await userManager.FindByEmailAsync("charlie@aemtek.com") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "CharlieLiu",
                    Email = "charlie@aemtek.com"
                };
                var result = await userManager.CreateAsync(user, "P@ssword1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "SUPER_ADMIN");
                }
            }
            if (await userManager.FindByEmailAsync("kimw@aemtek.com") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "KimWright",
                    Email = "kimw@aemtek.com"
                };
                var result = await userManager.CreateAsync(user, "P@ssword1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "CUSTOMER_ADMIN");
                }
            }
            if (await userManager.FindByEmailAsync("cliu336@yahoo.com") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "CharlieClient",
                    Email = "cliu336@yahoo.com"
                };
                var result = await userManager.CreateAsync(user, "P@ssword1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "CUSTOMER_COA");
                }
            }
        }

        private async static Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("SUPER_ADMIN"))
            {
                var role = new IdentityRole
                {
                    Name = "SUPER_ADMIN",
                };
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("CUSTOMER_ADMIN"))
            {
                var role = new IdentityRole
                {
                    Name = "CUSTOMER_ADMIN",
                };
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("CUSTOMER_COA"))
            {
                var role = new IdentityRole
                {
                    Name = "CUSTOMER_COA",
                };
                await roleManager.CreateAsync(role);
            }

            //if (!await roleManager.RoleExistsAsync("COA_DELIVERY"))
            //{
            //    var role = new IdentityRole
            //    {
            //        Name = "COA_DELIVERY",
            //        NormalizedName = "CoA delivery"
            //    };
            //    await roleManager.CreateAsync(role);
            //}
        }
    }
}

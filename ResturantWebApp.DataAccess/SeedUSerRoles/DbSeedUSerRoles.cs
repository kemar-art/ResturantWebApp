using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.Models;
using ResturantWebApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantWebApp.DataAccess.SeedUSerRoles
{
    public class DbSeedUSerRoles : IDbSeedUSerRoles
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbSeedUSerRoles(
            ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public void Initialize()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            // Checking if the role was already created if not then create the role
            if (! _roleManager.RoleExistsAsync(StaticDetail.KitchenRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(StaticDetail.KitchenRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetail.ManagerRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetail.FrontDeskRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetail.CustomerRole)).GetAwaiter().GetResult();


                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@localhost.com",
                    Email = "admin@localhost.com",
                    EmailConfirmed = true,
                    FirstName = "local",
                    LastName = "host"
                }, "Admin@1").GetAwaiter().GetResult();

                ApplicationUser? user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@localhost.com");

                _userManager.AddToRoleAsync(user, StaticDetail.ManagerRole).GetAwaiter().GetResult();

            }

            return;

        }
    }
}

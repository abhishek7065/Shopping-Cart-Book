using Microsoft.AspNetCore.Identity;
using Shopping_Cart_Book.Constants;

namespace Shopping_Cart_Book.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userMgr=service.GetService<UserManager<IdentityUser>>();    
            var roleMgr=service.GetService<RoleManager<IdentityRole>>();
            //adding some Roles to database
            await roleMgr.CreateAsync(new IdentityRole(Role.Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Role.User.ToString()));

            //create Admin User
            var admin = new IdentityUser()
            {
                UserName = "admin123@gmail.com",
                Email = "admin123@gmail.com",
                EmailConfirmed = true
            };

            var isUserExists = await userMgr.FindByEmailAsync(admin.Email);
            if(isUserExists is  null)
            {
                await userMgr.CreateAsync(admin, "Abhishek@123");
                await userMgr.AddToRoleAsync(admin, Role.Admin.ToString());
            }
          
        }
    }
}

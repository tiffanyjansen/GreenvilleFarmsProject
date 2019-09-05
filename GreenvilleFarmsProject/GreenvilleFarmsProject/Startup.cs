using Microsoft.Owin;
using Owin;
using GreenvilleFarmsProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Diagnostics;

[assembly: OwinStartupAttribute(typeof(GreenvilleFarmsProject.Startup))]
namespace GreenvilleFarmsProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateAdminUser();
        }

        /// <summary>
        /// This method creates the Admin User.
        /// </summary>
        public void CreateAdminUser()
        {
            //Set up variables
            ApplicationDbContext db = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //Check if Admin role already exists
            if (!roleManager.RoleExists("Admin"))
            {
                //Create the Admin Role
                var role = new IdentityRole { Name = "Admin" };
                roleManager.Create(role);

                //Get the password and stuff from the external file.
                string password = System.Web.Configuration.WebConfigurationManager.AppSettings["userPassword"];
                string email = System.Web.Configuration.WebConfigurationManager.AppSettings["userEmail"];

                //Create the user.
                var user = new ApplicationUser { Email = email, UserName = email };
                var checkUser = UserManager.Create(user, password);

                //Add default User to Role Admin   
                if (checkUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}

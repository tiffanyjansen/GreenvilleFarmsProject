using Microsoft.Owin;
using Owin;
using GreenvilleFarmsProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(GreenvilleFarmsProject.Startup))]
namespace GreenvilleFarmsProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUser();
        }

        /// <summary>
        /// This method creates the User.
        /// </summary>
        /// <returns>true, if succeeded; false, otherwise</returns>
        public bool CreateUser()
        {
            //Set up variables
            ApplicationDbContext db = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //Get the password and stuff from the external file.
            string password = System.Web.Configuration.WebConfigurationManager.AppSettings["userPassword"];
            string name = System.Web.Configuration.WebConfigurationManager.AppSettings["userName"];
            string email = System.Web.Configuration.WebConfigurationManager.AppSettings["userEmail"];

            //Create the user.
            var user = new ApplicationUser { Email = email, UserName = name };
            var checkUser = UserManager.Create(user, password);
            return checkUser.Succeeded;
        }
    }
}

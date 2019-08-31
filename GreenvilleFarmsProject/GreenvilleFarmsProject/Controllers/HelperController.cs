using GreenvilleFarmsProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenvilleFarmsProject.Controllers
{
    public class HelperController : Controller
    {
        /// <summary>
        /// Checks if logged in user is an admin.
        /// </summary>
        /// <param name="user">A user that will be check for admin status.</param>
        /// <returns>A boolean value.</returns>
        public bool IsAdminUser(System.Security.Principal.IIdentity user)
        {
            //Do the stuff
            ApplicationDbContext db = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //Get all of the roles associated with the user.
            var roles = UserManager.GetRoles(user.GetUserId());

            //Check if any of the roles are "Admin"
            if (roles.Count != 0)
            {
                foreach(var role in roles)
                {
                    if(role == "Admin")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
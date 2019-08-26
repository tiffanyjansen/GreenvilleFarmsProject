using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenvilleFarmsProject.Controllers
{
    public class AdminController : Controller
    {
        AdminHelperController adminHelper = new AdminHelperController();

        public ActionResult AddPicture()
        {
            //Check if user is an admin
            if (adminHelper.IsAdminUser(User.Identity))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && adminHelper != null)
            {
                adminHelper.Dispose();
                adminHelper = null;
            }

            base.Dispose(disposing);
        }
    }
}
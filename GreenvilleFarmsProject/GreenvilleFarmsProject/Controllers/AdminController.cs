using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreenvilleFarmsProject.Models;

namespace GreenvilleFarmsProject.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {
        AdminHelperController adminHelper = new AdminHelperController();
        DatabaseContext db = new DatabaseContext(); //The database connection.

        [HttpGet]
        public ActionResult AddPicture()
        {
            //Check if user is an admin
            if (adminHelper.IsAdminUser(User.Identity))
            {
                ViewBag.Message = TempData["Message"];
                return View(db.Pictures);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddPicture(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string FileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Images/Picture_Uploads"), FileName);
                    file.SaveAs(path);
                    db.Pictures.Add(new Picture { PictureName = FileName });
                    db.SaveChanges();
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View(db.Pictures);
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View(db.Pictures);
            }
        }

        public ActionResult RemovePicture(int id)
        {
            try
            {
                Picture pic = db.Pictures.Find(id);
                db.Pictures.Remove(pic);
                db.SaveChanges();
                TempData["Message"] = "The file has been removed.";
                return RedirectToAction("AddPicture", "Admin", new { db.Pictures });
            }
            catch
            {
                TempData["Message"] = "The file could not be removed.";
                return RedirectToAction("AddPicture", "Admin", new { db.Pictures });
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
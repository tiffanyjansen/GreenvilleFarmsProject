using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using GreenvilleFarmsProject.Models;

namespace GreenvilleFarmsProject.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {
        private HelperController helper = new HelperController(); //The helper controller with admin function
        private DatabaseContext db = new DatabaseContext(); //The database connection.

        [HttpGet]
        public ActionResult AddPicture()
        {            
            if (helper.IsAdminUser(User.Identity)) //Check if user is an admin
            {
                ViewBag.Message = TempData["Message"]; //return message if there is one
                ViewBag.Success = TempData["Success"];
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
            if (!helper.IsAdminUser(User.Identity)) //Make sure user is an admin user
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                try
                {
                    if (file.ContentLength > 0)
                    {
                        string FileName = Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/Content/Images/Picture_Uploads"), FileName); //Add file to this location
                        file.SaveAs(path);
                        db.Pictures.Add(new Picture { PictureName = FileName }); //Add file to database
                        db.SaveChanges();
                    }
                    ViewBag.Message = "Your picture was uploaded successfully."; //return success message
                    ViewBag.Success = true;
                    return View(db.Pictures);
                }
                catch
                {
                    ViewBag.Message = "We could not upload your picture at this time. Please try again."; //return error message
                    ViewBag.Success = false;
                    return View(db.Pictures);
                }
            }                
        }

        [HttpGet]
        public ActionResult RemovePicture(int? id)
        {
            if (!helper.IsAdminUser(User.Identity)) //Make sure user is an admin user
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (id == null)
                {
                    return RedirectToAction("AddPicture", "Admin", new { db.Pictures });
                }

                Picture pic = db.Pictures.Find(id);

                if (pic != null)
                {
                    return View(pic);
                }
                else
                {
                    return RedirectToAction("AddPicture", "Admin", new { db.Pictures });
                }
            }  
        }

        [HttpPost]
        public ActionResult RemovePicture(int id, int approved)
        {
            if (!helper.IsAdminUser(User.Identity)) //Make sure user is an admin user
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (approved == 1)
                {
                    try
                    {
                        Picture pic = db.Pictures.Find(id);

                        string fullPath = HostingEnvironment.MapPath("~/Content/Images/Picture_Uploads/" + pic.PictureName);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath); //Remove file from server
                        }
                        else
                        {
                            throw new Exception();
                        }
                                                
                        db.Pictures.Remove(pic); //Remove file from db
                        db.SaveChanges();
                        TempData["Message"] = "Your picture has been removed."; //return success message
                        TempData["Success"] = true;
                        return RedirectToAction("AddPicture", "Admin", new { db.Pictures });
                    }
                    catch
                    {
                        TempData["Message"] = "We could not remove your picture, please try again."; //return error message
                        TempData["Success"] = false;
                        return RedirectToAction("AddPicture", "Admin", new { db.Pictures });
                    }
                }
                else
                {
                    return RedirectToAction("AddPicture", "Admin", new { db.Pictures });
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            //Dispose of adminHelper
            if (disposing && helper != null)
            {
                helper.Dispose();
                helper = null;
            }

            //Dispose of db
            if (disposing && db != null)
            {
                db.Dispose();
                db = null;
            }

            base.Dispose(disposing);
        }
    }
}
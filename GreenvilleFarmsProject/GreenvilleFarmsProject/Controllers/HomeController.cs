using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using GreenvilleFarmsProject.Models;
using System.Threading.Tasks;

namespace GreenvilleFarmsProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Key = System.Web.Configuration.WebConfigurationManager.AppSettings["googleAPIkey"];
            return View();
        }

        public ActionResult About()
        {
            string url = "https://maps.googleapis.com/maps/api/js?key=" + System.Web.Configuration.WebConfigurationManager.AppSettings["googleAPIkey"];
            url += "&callback=initMap";
            ViewBag.googleAPI = url;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// This method is used to send emails fomr gvf.send@gmail.com to the email defined un the credential varible.
        /// </summary>
        /// <param name="contactForm">
        /// The contactForm is an object that holds the information from the user obtained through the Contact.cshtml View, and the ContactForm.cs model.
        /// </param>
        /// <returns>
        /// If the email is sent, it returns the Sent.cshtml View. Otherwise, it returns the Contact.cshtml.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email from: " + 
                    contactForm.Name + " (" + 
                    contactForm.Email + ")</p><p>Subject: " + 
                    contactForm.Subject + "</p><p>Message:\n" + 
                    contactForm.EmailContents + "</p>"; 
                var message = new MailMessage();
                message.To.Add(new MailAddress("gvf.contact@gmail.com"));
                message.From = new MailAddress("gvf.send@gmail.com");
                message.Subject = "Greenville Farms: Contact";
                message.Body = string.Format(body, contactForm.Name, contactForm.Email, contactForm.EmailContents);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = System.Web.Configuration.WebConfigurationManager.AppSettings["contactEmail"],
                        Password = System.Web.Configuration.WebConfigurationManager.AppSettings["contactPassword"]
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");

                }
            }
            return View(contactForm);
        }

        public ActionResult Sent()
        {
            return View();
        }
    }
}
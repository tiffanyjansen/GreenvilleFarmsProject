using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Net.Mail;
using GreenvilleFarmsProject.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GreenvilleFarmsProject.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            Picture[] pictures = db.Pictures.ToArray();
            ViewBag.Pictures = pictures;

            string key = System.Web.Configuration.WebConfigurationManager.AppSettings["googleAPIkey"];
            Review[] reviews = getReviews(key);
            IEnumerable<Review> list = new List<Review>(reviews);
            return View(list);
        }

        /// <summary>
        /// This function gets the google reviews using the API. 
        /// </summary>
        /// <param name="key">Our API key</param>
        /// <returns>The array of reviews</returns>
        private Review[] getReviews(string key)
        {
            //Build URL for GET request to Google's Place Details API, including key and parameters
            string url = "https://maps.googleapis.com/maps/api/place/details/json?placeid=" + "ChIJjabqtVQclVQR3B7E_iqiahQ" + "&fields=reviews&key=" + key;           

            //Get the response string by accessing the site and reading it all the way through.
            string responseString = GetResponseString(url);

            //Convert string into a json object
            var results = JsonConvert.DeserializeObject<PlaceDetail>(responseString);

            return results.result.reviews;
        }

        /// <summary>
        /// This method gets the response string for the APIs.
        /// </summary>
        /// <param name="url">The url for the APIs</param>
        /// <returns>The Json or XML object from the API</returns>
        private string GetResponseString(string url)
        {
            try
            {
                //Make a request using url, then grab response information 
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream information = response.GetResponseStream();
                StreamReader reader = new StreamReader(information);

                //Grab full response information, read to the end of the data and put it into a string
                return reader.ReadToEnd();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ActionResult Contact()
        {
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

        public ActionResult About()
        {
            //Google Map stuff
            string url = "https://maps.googleapis.com/maps/api/js?key=" + System.Web.Configuration.WebConfigurationManager.AppSettings["googleAPIkey"];
            url += "&callback=initMap";
            ViewBag.googleAPI = url;

            return View();
        }
    }
}
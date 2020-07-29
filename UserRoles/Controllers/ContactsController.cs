using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRoles.Models;
using System.Net.Mail;
using System.IO;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Configuration;

namespace UserRoles.Controllers
{
    public class ContactsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       
    
            // GET: Contacts
            [HttpGet]
        public  ActionResult Index()
        {
            
            return View();
        }

        private async Task configSendGridasync(Contact vm)
        {
            var apiKey = ConfigurationManager.AppSettings["SendGrid_Key"];
            var client = new SendGridClient("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");


            MailMessage ms = new MailMessage();
            //Email which you are getting 
            //from contact us page 
            var From = new EmailAddress(vm.Email);
            var to = new EmailAddress(ConfigurationManager.AppSettings["Email"].ToString()); //Where mail will be sent // You will need to make a Gmail for your business
            var Subject = vm.Subject;
            //var Body = "<br /><br />";
            var plainTextContent = vm.Message;
            var htmlContent = "Email sent from : " + vm.Name + ", Email Address: " + vm.Email + "" + vm.Message;
            // var IsBodyHtml = true;
            var msg = MailHelper.CreateSingleEmail(
                From,
                to,
                Subject,
                plainTextContent,
                htmlContent
                );
            var response = await client.SendEmailAsync(msg);
        }

        [HttpPost]
        public ActionResult Index(Contact vm)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var apiKey = ConfigurationManager.AppSettings["SendGrid_Key"];
                    var client = new SendGridClient("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");
                    SmtpClient smtp = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
                    smtp.Host = "smtp.sendgrid.net.com";
                    smtp.Port = 587;
                    //System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("noreply.codetek@gmail.com", "SendGrid_Key");

                    MailMessage ms = new MailMessage();
                    //Email which you are getting 
                    //from contact us page 
                    var to = new EmailAddress(vm.Email);
                    // var to = "noreply.codetek@gmail.com"; //new EmailAddress(ConfigurationManager.AppSettings["Email"].ToString()); //Where mail will be sent // You will need to make a Gmail for your business
                    var From = new EmailAddress(ConfigurationManager.AppSettings["Email"].ToString());
                    var Subject = vm.Subject;
                    //var Body = "<br /><br />";
                    var plainTextContent = vm.Message;
                    var htmlContent = "Email sent from : " + vm.Name + ", Email Address: " + vm.Email + "<br/><br/>" + vm.Message;
                    // var IsBodyHtml = true;
                    var msg = MailHelper.CreateSingleEmail(
                        to,
                        From,
                        Subject,
                        plainTextContent,
                        htmlContent
                        );
                    var response = client.SendEmailAsync(msg);
                   
                    ModelState.Clear();
                    ViewBag.Message = "Thank you for Contacting us ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
                }

            }
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult About()
        { return View(); }

    }
}

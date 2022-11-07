using FoodOrderingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;

namespace FoodOrderingSystem.Controllers
{

    public class PaymentController : Controller
    {
        private readonly AppDbContext _context;
        public IActionResult Payment(int? id)
        {
            HttpContext.Session.SetInt32("tp", (int)id);
           
            return View();
        }
        [HttpPost]
        public IActionResult Payment_Online(Payment payment)
        {
            ViewBag.TPOnline = HttpContext.Session.GetInt32("tp");
            if (ModelState.IsValid)
            {
                return View();

            }
            return View("~/Views/Payment/Payment.cshtml");

        }
        [HttpPost]
        public IActionResult Payment_Offline(Payment payment)
        {
            ViewBag.TPOffline = HttpContext.Session.GetInt32("tp");
            if (ModelState.IsValid)
            {
                return View();
            }
            return View("~/Views/Payment/Payment.cshtml");
        }
        [HttpPost]
        public IActionResult Success_Online(Payment_Online paymentonline)
        {
            ViewBag.TPOnline = HttpContext.Session.GetInt32("tp");
            if (ModelState.IsValid)
            {
                return View();

            }
            return View("~/Views/Payment/Payment_Online.cshtml");

        }

        public IActionResult Success_Offline()
        {
            ViewBag.TPOnline = HttpContext.Session.GetInt32("tp");
            
            try
            {
                //Create the msg object to be sent
                MailMessage msg = new MailMessage();
                //Add your email address to the recipients
                msg.To.Add("kanushisamrat@gmail.com");
                //Configure the address we are sending the mail from
                MailAddress address = new MailAddress("manupatel20052002@gmail.com");
                msg.From = address;
                msg.Subject = "Sample";
                msg.Body = "Example";
                msg.IsBodyHtml = false;

                //Configure an SmtpClient to send the mail.
                //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.EnableSsl = false;
                //client.Host = "relay-hosting.secureserver.net";
                //client.Port = 25;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("manupatel20052002@gmail.com", "rpyazznbinmkgenv");
                smtp.Send(msg);
                

                

                //Display some feedback to the user to let them know it was sent
                Console.WriteLine("Mail Sent");

                

            }
            catch (Exception ex)
            {
                //If the message failed at some point, let the user know
                Console.WriteLine(ex.ToString());
                //"Your message failed to send, please try again."
            }

            return View();
        }
    }
}

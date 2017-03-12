using System;
using System.Net.Mail;
using System.Web.Mvc;
using Xm.Trial.Models;

namespace Xm.Trial.Controllers
{
    public class ContactUsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new ContactUsForm());
        }

        [HttpPost]
        public ActionResult Index(ContactUsForm cuf)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(cuf.Email);
                    mailMessage.To.Add("feedback@gmail.com");
                    mailMessage.Subject = "User Feedback";
                    mailMessage.Body = cuf.Message;
                    SmtpClient smtpClient = new SmtpClient();

                    smtpClient.Host = "smtp.gmail.com";

                    smtpClient.Port = 587;

                    smtpClient.Credentials = new System.Net.NetworkCredential
                    ("feedback@gmail.com", "pass");

                    smtpClient.EnableSsl = true;

                    smtpClient.Send(mailMessage);

                    ModelState.Clear();
                    ViewBag.Message = "Thank you for Contacting us";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = "Whoops, some error occured";
                }
            }

            return View();
        }
    }
}
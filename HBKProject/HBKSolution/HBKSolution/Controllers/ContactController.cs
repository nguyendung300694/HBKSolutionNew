using HBKSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

namespace HBKSolution.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMail(EMailViewModel model)
        {
            //try
            //{
            //var message = new System.Net.Mail.MailMessage();
            //message.To.Add(new MailAddress("14110029@student.hcmute.edu.vn"));
            //message.Subject =  "Khách hàng '" + model.UserName + "' gửi email";
            //message.Body = "Email của '"+ model.UserName +"': "+ model.UserEmail +" " + "<br/>" +
            //    "Nội dung: " +model.EmailContent;
            //message.IsBodyHtml = true;
            //using (var smtp = new SmtpClient())
            //{
            //    smtp.Send(message);
            //}
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("email@gmail.com");
                mail.To.Add(new MailAddress("14110029@student.hcmute.edu.vn"));
                mail.Subject = "Khách hàng '" + model.UserName + "' gửi email";
                mail.Body = "Email của '" + model.UserName + "': " + model.UserEmail + " " + "<br/>" + "Nội dung: " +model.EmailContent;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("tanlongemailofficial@gmail.com", "A@123456A@");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return Json(new { success = true });
            //}
            //catch
            //{
            //    return Json(new { success = false });
            //}
        }
    }
}
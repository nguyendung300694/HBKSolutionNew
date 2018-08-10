using HBKSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mail;
using System.Web.Mvc;

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
            try
            {
                var message = new System.Net.Mail.MailMessage();
                message.To.Add(new MailAddress("nguyendung300694@gmail.com"));
                message.Subject =  "Khách hàng '" + model.UserName + "' gửi email";
                message.Body = "Email của '"+ model.UserName +"': "+ model.UserEmail +" " + "<br/>" +
                    "Nội dung: " +model.EmailContent;
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    smtp.Send(message);
                }
            }
            catch
            {
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }
    }
}
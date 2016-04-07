using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Weekly.Services.Abstract;

namespace Weekly.Services
{
    public class EmailService : IEmailService
    {
        public void SendAsync(MailMessage emailMessage)
        {
            using (var smtp = new SmtpClient())
            {
                emailMessage.From = new MailAddress("itservicedesk@pasar.com.ph");

                var credential = new NetworkCredential
                {
                    UserName = "itservicedesk@pasar.com.ph",  // replace with valid value
                    Password = "Abcd1234"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "mail.pasar.com.ph";
                //smtp.Port = 587;
                //smtp.EnableSsl = true;
                smtp.Send(emailMessage);
            }
        }
    }
}

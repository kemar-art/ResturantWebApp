using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ResturantWebApp.Utility
{
    public class EmailSender : IEmailSender
    {
        //When using send grid to send email
        /*public string SendGridSecret { get; set; }
        public EmailSender(IConfiguration _config)
        {
            SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
        }*/

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("kemarnorton@yahoo.com"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };


            // Sending the email
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.mail.yahoo.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("kemarnorton@yahoo.com", "pnaajtefmwznflps");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }

            return Task.CompletedTask;


            //resturantapp
            //GodJesus7$

            // There is send grid packadge that is needed to use SendGridClient
            /* var client = new SendGridClient(SendGridSecret);
             var from = new EmailAddress("hello@dotnetmastery.com", "Abby Food");
             var to = new EmailAddress(email);
             var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);


            
             return client.SendEmailAsync(msg);?*/

            //you have to put the security key in the appsettings.json file then call it above.
            //This was an example key I will have to get mine from send grid
            /*"SendGrid": {
                "SecretKey": "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"
             }*/

        }
    }
}

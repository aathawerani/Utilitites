using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace emailutility
{
    class EmailSender
    {

        public void Send(string senderEmail, string recipientEmail, string subject, string body, string attachmentFilePath, string smtpServer, int smtpPort, string senderPassword) {
                // Create a new mail message
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail);
                mail.To.Add(recipientEmail);
                mail.Subject = subject;
                mail.Body = body;

                // Add attachment if it exists
                if (!string.IsNullOrEmpty(attachmentFilePath) && System.IO.File.Exists(attachmentFilePath))
                {
                    Attachment attachment = new Attachment(attachmentFilePath);
                    mail.Attachments.Add(attachment);
                }

                // Configure the SMTP client
                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtpClient.EnableSsl = true;

                // Send the email
                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully!");
        }
    }
}

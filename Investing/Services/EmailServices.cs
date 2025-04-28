using System;
using System.Net.Mail;
using System.Net;

namespace Investing.Services
{
    public class EmailServices
    {
        public string GenerateConfirmationCode()
        {
            // Генерация случайного 6-значного кода
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
        public void SendConfirmationEmail(string email, string confirmationCode)
        {
            var fromAddress = new MailAddress("your-email@example.com", "Your App Name");
            var toAddress = new MailAddress(email);
            const string subject = "Confirm your registration";
            string body = $"Your confirmation code is: {confirmationCode}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com", // Укажите SMTP-сервер
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "your-email-password")
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            //2345Ayi_
            //814DE6AD-EDA4-2A44-23AB-02EF37437B32
        }
    }
}

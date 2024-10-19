
using System.Net;
using System.Net.Mail;

namespace SendEmail.Repositories;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var mail = "mahammadmstfyv1234@outlook.com";
        var pw = "mcixvhjwaciqjrju";

        var client = new SmtpClient("smtp-mail.outlook.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(mail, pw)
        };

        return client.SendMailAsync(new MailMessage(
                      from: mail,
                      to: email,
                      subject,
                      message
                  ));
    }
}

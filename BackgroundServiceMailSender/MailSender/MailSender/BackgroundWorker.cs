

using MailKit.Net.Smtp;
using Microsoft.Extensions.Hosting;
using MimeKit;

namespace MailSender;

internal class BackgroundWorker : BackgroundService
{
    //Timer timer;
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //timer = new Timer(SendMail
        //                  , new MailData("mahammadvm@code.edu.az", "Report", "Hi my dear")
        //                  , TimeSpan.Zero
        //                  , TimeSpan.FromSeconds(10));
        SendMail(new MailData("mahammadvm@code.edu.az", "Report", "Hi my dear"));
        return Task.CompletedTask;
    }
    private void SendMail(object data)
    {
        
        Console.WriteLine("Email Sending...");
        var email = new MimeMessage();
        MailData mailData = (MailData)data;
        

        email.Sender = MailboxAddress.Parse(EmailConfig.email);
        email.To.Add(MailboxAddress.Parse(mailData.To));
        email.Subject=mailData.Subject;
        var builder = new BodyBuilder();
        builder.HtmlBody = $"<h1 style='text-align:center;'>{mailData.Body}</h1>";
        email.Body=builder.ToMessageBody();
        using (SmtpClient smtp= new())
        {
            smtp.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(EmailConfig.email, EmailConfig.password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
        Console.WriteLine("email sent");

    }
}
record MailData(string To,string Subject, string Body);

 class EmailConfig
{
    public static string email = "mahammadvm@hotmail.com";
    public static string password = "Mmustafayev2001";
}
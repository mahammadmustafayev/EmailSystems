using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;


namespace SendEmail.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public EmailController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public async Task SendEmailAsync(string toEmail, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration["Hotmail:Email"]));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = "Test Email";
        email.Body = new TextPart(TextFormat.Html) { Text = body };

        using (var smtp = new SmtpClient())
        {
            try
            {
                // SMTP serverinə qoşulma
                await smtp.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);

                // Tətbiq parolu ilə autentifikasiya
                await smtp.AuthenticateAsync(_configuration["Hotmail:Email"], _configuration["Hotmail:Password"]);

                // E-poçtun göndərilməsi
                await smtp.SendAsync(email);

                // Bağlantının qapadılması
                await smtp.DisconnectAsync(true);

                Console.WriteLine("E-poçt uğurla göndərildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E-poçt göndərilmədi: {ex.Message}");
            }
        }
    }
}

using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace SimpleEmailApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private  readonly  IConfiguration _configuration ;
    public EmailController(IConfiguration configuration)
    {
        _configuration= configuration;
    }
    [HttpPost]
    public async Task<IActionResult> SendEmail(string mail,string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration["mailName"]));
        email.To.Add(MailboxAddress.Parse(mail));
        email.Subject = "Test";
        email.Body= new TextPart(TextFormat.Html) { Text= body };

        using (SmtpClient smtp = new())
        {
            smtp.Connect("smtp.office365.com", 587,SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration["mailName"], _configuration["mailPassword"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
        return Ok("good");

    }
}

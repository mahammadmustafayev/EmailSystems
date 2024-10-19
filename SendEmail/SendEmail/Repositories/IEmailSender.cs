namespace SendEmail.Repositories;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
}

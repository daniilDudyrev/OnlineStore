namespace OnlineStore.Domain.Services;

public interface IEmailSender
{
    Task Send(string toEmail,string subject,string htmlBody);
}
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace OnlineStore.Domain.Services;

public class EmailSender : IEmailSender, IDisposable,IAsyncDisposable
{
    private readonly SmtpClient _smtpClient = new();
    private readonly SmtpConfig _smtpConfig;

    public EmailSender(IOptionsSnapshot<SmtpConfig> options)
    {
        _smtpConfig = options.Value;
    }
    public async Task Send(string toEmail, string subject, string htmlBody)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_smtpConfig.UserName));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html)
        {
            Text = htmlBody
        };
        await EnsureConnectedAndAuthenticated();
        await _smtpClient.SendAsync(email);
    }

    private async Task EnsureConnectedAndAuthenticated()
    {
        if (!_smtpClient.IsConnected)
        {
            await _smtpClient.ConnectAsync(_smtpConfig.Host, _smtpConfig.Port);
        }

        if (!_smtpClient.IsAuthenticated)
        {
            await _smtpClient.AuthenticateAsync(_smtpConfig.UserName, _smtpConfig.Password);
        }
    }

    public void Dispose()
    {
        _smtpClient.Disconnect(true);
        _smtpClient.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_smtpClient.IsConnected)
        {
            await _smtpClient.DisconnectAsync(true);
        }
        _smtpClient.Dispose();
    }
}

public class SmtpConfig
{
    public string Host { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int Port { get; set; }
}

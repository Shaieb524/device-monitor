namespace CustomSerives;  

using System.Net;
using System.Net.Mail;
using CustomModels;
using Microsoft.Extensions.Logging;

class MailServices
{
    private readonly ILogger _logger;

    public MailServices(ILogger logger)
    {
        _logger = logger;
    }

    public void SendEmail(MailOptions mOptions)
    {
        MailMessage mail = new MailMessage(mOptions.FromEmail, mOptions.ToEmail)
        {
            Subject = "Windows Startup Notification",
            Body = "Windows has started up."
        };

        SmtpClient smtpClient = new SmtpClient(mOptions.SmtpServer)
        {
            Port = mOptions.SmtpPort,
            Credentials = new NetworkCredential(mOptions.Username, mOptions.Password),
            EnableSsl = true // Use SSL for secure connection
        };

        try
        {
            smtpClient.Send(mail);
            _logger.LogInformation("Email sent successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error sending email: {ex.Message}");
        }
    }
}
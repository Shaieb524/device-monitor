namespace CustomSerives;  

using System.Net;
using System.Net.Mail;
using CustomModels;

class MailServices
{
    public static void SendEmail(MailOptions mOptions)
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
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }
}
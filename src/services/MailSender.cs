namespace CustomSerives;  

using System.Net;
using System.Net.Mail;
using CustomModels;

class MailServices
{
    // private string? SmtpServer {get; set;}
    // private int SmtpPort {get; set;} 
    // private string? FromEmail {get; set;} 
    // private string? ToEmail {get; set;} 
    // private string? Username {get; set;} 
    // private string? Password {get; set;} //"xxxyneunykzuiend"; 

    // public MailServices(string smtpServer, int smtpPort, 
    //     string fromEmail, string toEmail, string username, string password)
    // {
    //     this.SmtpServer = smtpServer;
    //     this.SmtpPort = smtpPort;
    //     this.FromEmail = fromEmail;
    //     this.ToEmail = toEmail;
    //     this.Username = username;
    //     this.Password = password;
    // }   

    public static void SendEmail(MailOptions mOptions)
    {
        // Email message
        MailMessage mail = new MailMessage(mOptions.FromEmail, mOptions.ToEmail);
        mail.Subject = "Windows Startup Notification";
        mail.Body = "Windows has started up.";

        // SMTP client
        SmtpClient smtpClient = new SmtpClient(mOptions.SmtpServer);
        smtpClient.Port = mOptions.SmtpPort;
        smtpClient.Credentials = new NetworkCredential(mOptions.Username, mOptions.Password);
        smtpClient.EnableSsl = true; // Use SSL for secure connection

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
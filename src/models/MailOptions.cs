namespace CustomModels;

class MailOptions
{
    public string SmtpServer {get; set;} = "smtp.gmail.com";
    public int SmtpPort {get; set;} = 587;
    public string FromEmail {get; set;} = "femailexample@ex.com";
    public string ToEmail {get; set;} = "toemailexample@ex.com";
    public string Username {get; set;} = "email@ex.com";
    public string Password {get; set;} = "sampleEmailAccountAppPassword";
}
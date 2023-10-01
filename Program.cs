using CustomModels;
using CustomSerives;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main()
    {

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddUserSecrets<Program>();

        IConfiguration config = builder.Build();
        
        MailOptions mailOptions = new MailOptions();

        config.GetSection("MailOptions").Bind(mailOptions);
        mailOptions.Password = config["MailOptions:Password"]!;

        MailServices.SendEmail(mailOptions);
    }
}
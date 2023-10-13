using CustomModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace startup_checker
{
    internal class Worker
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private static MailOptions _mailOptions;

        public Worker(IConfiguration config, ILogger<Worker> logger, MailOptions mailOptions)
        {
            _configuration = config;
            _logger = logger;
            _mailOptions = mailOptions;
        }

        public void Run()
        {
            _logger.LogInformation("Heyyyy");
            _configuration.GetSection("MailOptions").Bind(_mailOptions);
            LidMonitor.MainCall(_mailOptions);

            Console.WriteLine("Worker is here");
        }
    }
}

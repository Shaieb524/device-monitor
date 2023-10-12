using CustomModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace startup_checker
{
    internal class Worker
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public Worker(IConfiguration config, ILogger<Worker> logger)
        {
            _configuration = config;
            _logger = logger;
        }
        public void Run()
        {
            _logger.LogInformation("Heyyyy");

            MailOptions mailOptions = new MailOptions();

            _configuration.GetSection("MailOptions").Bind(mailOptions);
            LidMonitor.MainCall(mailOptions);

            Console.WriteLine("Worker is here");
        }
    }
}

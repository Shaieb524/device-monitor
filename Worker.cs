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
        private static LidMonitor _lidMonitor;

        public Worker(IConfiguration config, ILogger<Worker> logger, MailOptions mailOptions, LidMonitor lidMonitor)
        {
            _configuration = config;
            _logger = logger;
            _mailOptions = mailOptions;
            _lidMonitor = lidMonitor;
        }

        public void Run()
        {
            _logger.LogInformation("Worker is here");
            _configuration.GetSection("MailOptions").Bind(_mailOptions);
            _lidMonitor.StartMonitoring(_mailOptions);

        }
    }
}

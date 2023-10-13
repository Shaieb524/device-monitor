using CustomModels;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

class LidMonitor
{
    private static MailOptions _mailOptions;
    private const string LOG_FILE_PATH = "user_activity.txt";
    private readonly ILogger _logger;

    public LidMonitor(ILogger logger)
    {
        _logger = logger;
    }

    public void StartMonitoring(MailOptions mailOptions)
    {
        try
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            _logger.LogInformation("Monitoring lock.");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred: " + ex.Message);
        }
    }

    void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
    {
        DateTime currentDateAndTime = DateTime.Now;
        string dateOnly = currentDateAndTime.ToString("MM/dd/yyyy");
        string todayLogPath = $"logs/{dateOnly.Replace("/", "-").Replace(" ", "-")}/{LOG_FILE_PATH}";

        if (e.Reason == SessionSwitchReason.SessionLock)
        {
            _logger.LogInformation("I left my desk.");
            EnsureDirectoryExists(GetFullFilePath(todayLogPath));
            AppendMessageToFile(GetFullFilePath(todayLogPath), "Desktop closed!");
        }
        else if (e.Reason == SessionSwitchReason.SessionUnlock)
        {
            _logger.LogInformation("I returned to my desk.");
            EnsureDirectoryExists(GetFullFilePath(todayLogPath));
            AppendMessageToFile(GetFullFilePath(todayLogPath), "Desktop opened!");
        }
    }

    void AppendMessageToFile(string filePath, string message)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{ DateTime.Now + " : " + message}");
            }
        } 
        catch (Exception ex) 
        {
            _logger.LogError("An error occurred while writing to the file: " + ex.Message);
        }
    }

    static string GetFullFilePath(string relativeFilePath)
    {
        string projectRootPath = Directory.GetCurrentDirectory();

        return Path.Combine(projectRootPath, relativeFilePath);
    }

    static void EnsureDirectoryExists(string filePath)
    {
        string directoryPath = Path.GetDirectoryName(filePath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }
}
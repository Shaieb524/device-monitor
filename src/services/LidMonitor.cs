using CustomModels;
using Microsoft.Win32;

class LidMonitor
{
    private static MailOptions _mailOptions;
    private const string LOG_FILE_PATH = "user_activity.txt";

    public static void MainCall(MailOptions mailOptions)
    {
        try
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            Console.WriteLine("monitoring lock");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    static void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
    {
        DateTime currentDateAndTime = DateTime.Now;
        string dateOnly = currentDateAndTime.ToString("MM/dd/yyyy");
        string todayLogPath = $"logs/{dateOnly.Replace("/", "-").Replace(" ", "-")}/{LOG_FILE_PATH}";

        if (e.Reason == SessionSwitchReason.SessionLock)
        {
            Console.WriteLine("I left my desk");
            EnsureDirectoryExists(GetFullFilePath(todayLogPath));
            AppendMessageToFile(GetFullFilePath(todayLogPath), "Desktop closed!");
        }
        else if (e.Reason == SessionSwitchReason.SessionUnlock)
        {
            Console.WriteLine("I returned to my desk");
            EnsureDirectoryExists(GetFullFilePath(todayLogPath));
            AppendMessageToFile(GetFullFilePath(todayLogPath), "Desktop opened!");
        }
    }

    static void AppendMessageToFile(string filePath, string message)
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
            Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
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
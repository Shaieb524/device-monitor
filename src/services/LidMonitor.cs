using CustomModels;
using Microsoft.Win32;

class LidMonitor
{
    private static MailOptions _mailOptions;

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
        if (e.Reason == SessionSwitchReason.SessionLock)
        {
            Console.WriteLine("I left my desk");
        }
        else if (e.Reason == SessionSwitchReason.SessionUnlock)
        {
            Console.WriteLine("I returned to my desk");

        }
    }
}
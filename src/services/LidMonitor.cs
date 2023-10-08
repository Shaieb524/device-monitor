using System;
using System.Diagnostics;
using System.IO;
using CustomModels;
using CustomSerives;
using Microsoft.Win32;

class LidMonitor
{
    private static MailOptions _mailOptions;
    //private FileStream fileStream = new FileStream(@"c:\test.txt", FileMode.OpenOrCreate);

    public static void MainCall(MailOptions mailOptions)
    {
        EventLog[] eventLogs = EventLog.GetEventLogs();

        //SystemEvents.PowerModeChanged += OnPowerModeChanged;
        SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(OnPowerModeChanged);
        Console.WriteLine("Monitoring laptop lid events. Press Enter to exit...");
        Console.ReadLine();
    }

    private static void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
    {
        if (e.Mode == PowerModes.Suspend)
        {
            Console.WriteLine("Laptop is going to sleep (lid closed).");
            using (StreamWriter writer = new StreamWriter("./monitor.txt"))
            {
                writer.WriteLine("Laptop is going to sleep ");
            }
            // Implement your code for sleep event here
        }
        else if (e.Mode == PowerModes.Resume)
        {
            Console.WriteLine("Laptop is waking up (lid opened).");
            using (StreamWriter writer = new StreamWriter("./monitor.txt"))
            {
                writer.WriteLine("Laptop is waking up");
            }
            // Implement your code for wake event here
        }
        using (StreamWriter writer = new StreamWriter("./monitor.txt"))
        {
            writer.WriteLine("nothing");
        }
    }

}
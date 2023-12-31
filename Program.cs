﻿using CustomModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using startup_checker;
using System.Diagnostics;

class Program
{
    public static void Main(string[] args)
    {
        try
        {
            IHost host = CreateHostBuilder(args).Build();
            // create separate worker class with DI
            var worker = ActivatorUtilities.CreateInstance<Worker>(host.Services);
            worker.Run();
        }
        catch (Exception ex)
        {
            EventLog.WriteEntry("Application testtt", ex.ToString(), EventLogEntryType.Error);
        }
    }
        
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddUserSecrets<Program>();
            })
            .ConfigureServices(services =>
            {
                services.AddSingleton<MailOptions>();
                services.AddSingleton<LidMonitor>();
            })
            .UseWindowsService();


    }
}


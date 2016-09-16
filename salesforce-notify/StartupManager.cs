﻿using System;
using System.Security.Principal;
using Microsoft.Win32;
using System.Configuration;

namespace salesforce_notify
{
    public static class StartUpManager
    {
        public static string AppName;

        static StartUpManager()
        {
            AppName = ConfigurationManager.AppSettings["appName"];
        }
        public static void AddApplicationToCurrentUserStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue(AppName, "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
            }
        }

        public static void AddApplicationToAllUserStartup()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue(AppName, "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
            }
        }

        public static void RemoveApplicationFromCurrentUserStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue(AppName, false);
            }
        }

        public static void RemoveApplicationFromAllUserStartup()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue(AppName, false);
            }
        }

        public static bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            return isAdmin;
        }


    }


    //public class StartupManager
    //{
    //    public RegistryKey AppRegistryKey { get; private set; }
    //    public bool IsStartupEnabled
    //    {
    //        get { return AppRegistryKey?.GetValue(_appName) == null;}
    //        private set
    //        {

    //        }
    //    }

    //    private readonly string _appName;
    //    public StartupManager(string appName)
    //    {
    //        _appName = appName;
    //        AppRegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
    //    }

    //    public void Enable()
    //    {
    //        if(IsStartupEnabled) return;
    //        AppRegistryKey.SetValue(_appName, Application.ExecutablePath);
    //    }

    //    public void Disable()
    //    {
    //        if(!IsStartupEnabled) return;
    //        AppRegistryKey.DeleteValue(_appName);
    //    }


    //}
}

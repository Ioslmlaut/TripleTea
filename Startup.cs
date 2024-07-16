using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TripleTea
{
    internal class Startup
    {
        public static void AddToCurrentUser()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                string loc = System.Reflection.Assembly.GetExecutingAssembly().Location;
                loc = loc.Replace("TripleTea.dll", "TripleTea.exe");
                key.SetValue("TripleTea", "\"" + loc + "\"");
            }
        }

        public static void AddToAllUsers()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                string loc = System.Reflection.Assembly.GetExecutingAssembly().Location;
                loc = loc.Replace("TripleTea.dll", "TripleTea.exe");
                key.SetValue("TripleTea", "\"" + loc + "\"");
            }
        }

        public static void RemoveFromCurrentUser()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue("TripleTea", false);
            }
        }

        public static void RemoveFromAllUsers()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue("TripleTea", false);
            }
        }

        public static bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                //get the currently logged in user
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
}

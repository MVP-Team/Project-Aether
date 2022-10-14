using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Aether_Console.Terminal
{
    public class Applications
    {
        public static IDictionary<string, string> allApplications = new Dictionary<string, string>();
       
        static Applications()
        {
            FindDirectoryWithProgramm();
        }


        private static void FindDirectoryWithProgramm()
        {
            string displayName;
            string directoryName;

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false))
            {
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    directoryName = subkey.GetValue("InstallLocation") as string;
                    if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(directoryName))
                        continue;
                    allApplications.Add(displayName, directoryName);
                }
            }

            //using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false))
            using (var localMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                var key = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false);
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    directoryName = subkey.GetValue("InstallLocation") as string;
                    if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(directoryName))
                        continue;
                    if (allApplications.ContainsKey(displayName))
                        continue;

                    allApplications.Add(displayName, directoryName);
                }
            }

            using (var localMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            {
                var key = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false);
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    directoryName = subkey.GetValue("InstallLocation") as string;
                    if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(directoryName))
                        continue;
                    if (allApplications.ContainsKey(displayName))
                        continue;

                    allApplications.Add(displayName, directoryName);
                }
            }

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall", false))
            {
                foreach (String keyName in key.GetSubKeyNames())
                {
                    RegistryKey subkey = key.OpenSubKey(keyName);
                    displayName = subkey.GetValue("DisplayName") as string;
                    directoryName = subkey.GetValue("InstallLocation") as string;
                    if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(directoryName))
                        continue;
                    if (allApplications.ContainsKey(displayName))
                        continue;

                    allApplications.Add(displayName, directoryName);
                }
            }

            foreach (var d in System.IO.Directory.GetDirectories(@"C:\Program Files (x86)"))
            {
                var dirName = new DirectoryInfo(d).Name;
                if (!allApplications.ContainsKey(dirName))
                {
                    allApplications.Add(dirName, d);
                }
            }

            foreach (var d in System.IO.Directory.GetDirectories(@"C:\Program Files"))
            {
                var dirName = new DirectoryInfo(d).Name;
                if (!allApplications.ContainsKey(dirName))
                {
                    allApplications.Add(dirName, d);
                }
            }
        }
    }
}

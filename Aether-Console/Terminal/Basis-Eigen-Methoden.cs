
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using static System.Net.WebRequestMethods;

namespace Aether_Console.Terminal
{
    partial class Basis
    {
        /*Please write your methods here*/
        private static void Application(string app)
        {
            string directory = FindDirectoryByProgramm(ref app);
            //Console.WriteLine(programmRun(app, directory));
            try
            {
                Process.Start(programmRun(app, directory));
            } catch (Exception ex)
            {
                Console.WriteLine("Sorry there is no Application!");
            }
        }






        //This code organizes an application call through an exe path.
        private static string FindDirectoryByProgramm(ref string app)
        {
            string displayName;
            RegistryKey key;
            List<RegistryKey> validKeys = new();

            // search in: CurrentUser
            key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string ?? "";
                if (displayName.Contains(app))
                {
                    validKeys.Add(subkey);
                    return subkey.GetValue("InstallLocation")?.ToString();
                }
            }
            foreach(RegistryKey kia in validKeys)
            {
                if(kia.GetValue("DisplayName") == app)
                {
                    app = kia.GetValue("DisplayName") as string;
                    return kia.GetValue("InstallLocation")?.ToString();
                }
            } 
            if(validKeys.Count > 0)
            {
                app = validKeys[0].GetValue("DisplayName") as string;
                return validKeys[0].GetValue("InstallLocation")?.ToString();
            }


            // search in: LocalMachine_32
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string ?? "";

                if (displayName.Contains(app))
                {
                    return subkey.GetValue("InstallLocation")?.ToString();
                }
            }
            foreach (RegistryKey kia in validKeys)
            {
                if (kia.GetValue("DisplayName") == app)
                {
                    app = kia.GetValue("DisplayName") as string;
                    return kia.GetValue("InstallLocation")?.ToString();
                }
            }
            if (validKeys.Count > 0)
            {
                app = validKeys[0].GetValue("DisplayName") as string;
                return validKeys[0].GetValue("InstallLocation")?.ToString();
            }

            // search in: LocalMachine_64
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string ?? "";
                if (displayName.Contains(app))
                {
                    return subkey.GetValue("InstallLocation")?.ToString();
                }
            }
            foreach (RegistryKey kia in validKeys)
            {
                if (kia.GetValue("DisplayName") == app)
                {
                    app = kia.GetValue("DisplayName") as string;
                    return kia.GetValue("InstallLocation")?.ToString();
                }
            }
            if (validKeys.Count > 0)
            {
                app = validKeys[0].GetValue("DisplayName") as string;
                return validKeys[0].GetValue("InstallLocation")?.ToString();
            }
            //key = 
            return "";
        }


        private static string programmRun(string app, string directory)
        {
            string right = "";
            string exe = app;
            Console.WriteLine(directory);
            if (Directory.GetFiles(directory, $"{exe.ToLower()}.exe", SearchOption.AllDirectories).Length != 0)
            {
                string[] directories = Directory.GetFiles(directory, $"{exe.ToLower()}.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"1:{right}");
            }
            else if (Directory.GetFiles(directory, $"*{exe.ToLower()}.exe", SearchOption.AllDirectories).Length != 0)
            {
                string[] directories = Directory.GetFiles(directory, $"*{exe.ToLower()}.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"2:{right}");
            } else if (Directory.GetFiles(directory, "launcher.exe", SearchOption.AllDirectories).Length != 0)
            {
                string[] directories = Directory.GetFiles(directory, "launcher.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"3:{right}");
            } else if (Directory.GetFiles(directory, "Code.exe", SearchOption.AllDirectories).Length != 0)
            {
                string[] directories = Directory.GetFiles(directory, "Code.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"4:{right}");
            } else if (Directory.GetFiles(directory, $"{exe.Replace(" ", "")}.exe", SearchOption.AllDirectories).Length != 0) {
                string[] directories = Directory.GetFiles(directory, $"{exe.Replace(" ", "")}.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"5:{right}");
            }
            return right;
        }   
    }
}

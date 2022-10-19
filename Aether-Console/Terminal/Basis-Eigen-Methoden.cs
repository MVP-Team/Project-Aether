
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using static System.Net.WebRequestMethods;

namespace Aether_Console.Terminal
{
    partial class Basis
    {

        public static IDictionary<string, string> applications;
        static Basis()
        {
            applications = Applications.allApplications;
            Office();
        }
        /*Please write your methods here*/

        public static void Search(string term)
        {
            string search = $"https://www.google.com/search?q={term}";

            try
            {
                Process.Start(search);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    search = search.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(search) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", search);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", search);
                }
                else
                {
                    throw;
                }
            }
        }
        public static void Application(string app)
        {
            try
            {
                Process.Start($"{app.ToLower()}.exe");
            }
            catch
            {

                string? directory = null;
                try
                {
                    directory = FindDirectoryByName(app);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (directory != null)
                {
                    try
                    {
                        Process.Start(programmRun(app, directory));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Sorry there is no such Application!");
                    }
                }
            }

        }

        private static void Office()
        {
            applications.Add("Word", "C:\\Program Files\\Microsoft Office\\root\\Office16");
            applications.Add("Excel", "C:\\Program Files\\Microsoft Office\\root\\Office16");
            applications.Add("PowerPoint", "C:\\Program Files\\Microsoft Office\\root\\Office16");
            applications.Add("Access", "C:\\Program Files\\Microsoft Office\\root\\Office16");
            applications.Add("Publisher", "C:\\Program Files\\Microsoft Office\\root\\Office16");
            applications.Add("OneNote", "C:\\Program Files\\Microsoft Office\\root\\Office16");
            applications.Add("Outlook", "C:\\Program Files\\Microsoft Office\\root\\Office16");
        }


        private static string FindDirectoryByName(string app)
        {
            if (app.Length < 3)
            {
                throw new ArgumentException("Sorry there is no Application!");
            }
            else if (applications.ContainsKey(app))
            {
                return applications[app];
            }
            foreach (string apps in applications.Keys)
            {
                if (apps.Contains(app))
                {
                    return applications[apps];
                }
            }
            throw new ArgumentException("Sorry there is no Application!");
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
            }
            else if (Directory.GetFiles(directory, "launcher.exe", SearchOption.AllDirectories).Length != 0)
            {
                string[] directories = Directory.GetFiles(directory, "launcher.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"3:{right}");
            }
            else if (Directory.GetFiles(directory, "Code.exe", SearchOption.AllDirectories).Length != 0)
            {
                string[] directories = Directory.GetFiles(directory, "Code.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"4:{right}");
            }
            else if (Directory.GetFiles(directory, $"{exe.Replace(" ", "")}.exe", SearchOption.AllDirectories).Length != 0)
            {
                string[] directories = Directory.GetFiles(directory, $"{exe.Replace(" ", "")}.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"5:{right}");
            }
            else if (Directory.GetFiles(directory, $"{exe.Substring(0, 6).ToUpper()}*.exe", SearchOption.AllDirectories).Length != 0)
            {
                string[] directories = Directory.GetFiles(directory, $"{exe.Substring(0, 6).ToUpper()}*.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"6:{right}");
            }
            else if (Directory.GetFiles(directory, $"{exe.Substring(0, exe.IndexOf(" ") - 2).ToLower()}*.exe", SearchOption.AllDirectories).Length != 0)
            {
                string[] directories = Directory.GetFiles(directory, $"{exe.Substring(0, exe.IndexOf(" ") - 2).ToLower()}*.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"7:{right}");
            }
            else if (Directory.GetFiles(directory, $"*{exe.Substring(exe.IndexOf(" ") + 1, exe.Length - exe.IndexOf(" ") - 2).ToLower()}*.exe", SearchOption.AllDirectories).Length != 0)
            {
                string[] directories = Directory.GetFiles(directory, $"*{exe.Substring(exe.IndexOf(" ") + 1, exe.Length - exe.IndexOf(" ") - 2).ToLower()}*.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"8:{right}");
            }

            return right;
        }
    }
}

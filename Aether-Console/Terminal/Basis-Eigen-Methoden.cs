
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

        public static IDictionary<string, string> applications;
        static Basis()
        {
         applications = Applications.allApplications;
        }
        /*Please write your methods here*/
        public static void Application(string app)
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
            //Console.WriteLine(programmRun(app, directory));

            if (directory != null)
            {
                try
                {
                    Process.Start(programmRun(app, directory));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Sorry there is no Application!");
                }
            }
            
        }


        
       
        private static string FindDirectoryByName(string app)
        {
            if(app.Length < 3)
            {
                throw new ArgumentException("Sorry there is no Application!");
            } else if (applications.ContainsKey(app))
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
            else if (Directory.GetFiles(directory, $"{exe.ToUpper()}.exe", SearchOption.AllDirectories).Length != 0)
            {
                string[] directories = Directory.GetFiles(directory, $"{exe.ToUpper()}.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"3:{right}");
            }
            else if (Directory.GetFiles(directory, $"*{exe.ToUpper()}.exe", SearchOption.AllDirectories).Length != 0)
            {
                string[] directories = Directory.GetFiles(directory, $"*{exe.ToUpper()}.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"4:{right}");
            }
            else if (Directory.GetFiles(directory, "launcher.exe", SearchOption.AllDirectories).Length != 0)
            {
                string[] directories = Directory.GetFiles(directory, "launcher.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"5:{right}");
            } else if (Directory.GetFiles(directory, "Code.exe", SearchOption.AllDirectories).Length != 0)
            {
                string[] directories = Directory.GetFiles(directory, "Code.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"6:{right}");
            } else if (Directory.GetFiles(directory, $"{exe.Replace(" ", "")}.exe", SearchOption.AllDirectories).Length != 0) {
                string[] directories = Directory.GetFiles(directory, $"{exe.Replace(" ", "")}.exe", SearchOption.AllDirectories);
                foreach (string file in directories)
                {
                    right = file;
                }
                Console.WriteLine($"7:{right}");
            }
            return right;
        }   
    }
}

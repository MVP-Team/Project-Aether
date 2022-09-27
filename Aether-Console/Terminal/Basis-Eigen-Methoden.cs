
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
            IDictionary<string, string> humans = new Dictionary<string, string>();
            humans.Add("Internet-Explorer", "Explorer");
            humans.Add("Edge", "Microsoft");
            IDictionary<string, string> apps = new Dictionary<string, string>();
            apps.Add("Explorer", "iexplore.exe");
            apps.Add("Firefox", "firefox.exe");
            apps.Add("Microsoft", "msedge.exe");
            apps.Add("Origin", "Origin.exe");
            

            try
            {
                Process.Start(programmRun(app, apps[app]));
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Sorry Aether can't find your application! :(");
            }
        } 





        //This code organizes an application call through an exe path.
        private static string programmRun(string programm, string exe)
        {
            string right = "";

            DirectoryInfo selDirectory = new DirectoryInfo(@"C:\Program Files (x86)");
            DirectoryInfo[] dirsInDir = selDirectory.GetDirectories("*" + programm + "*.*");

            string? fsName = null;
            foreach (DirectoryInfo foundDir in dirsInDir)
            {
                string fullName = foundDir.FullName;
                fsName = fullName;
                if (Directory.GetFiles(fullName, exe, SearchOption.AllDirectories) != null)
                {
                    break;
                }
                
            }
       
            
            DirectoryInfo secDirectory = new DirectoryInfo(@"C:\Program Files");
            DirectoryInfo[] dirsSecInDir = secDirectory.GetDirectories("*" + programm + "*.*");
            string? scName = null;
            foreach (DirectoryInfo foundDir in dirsSecInDir)
            {
                string fullName = foundDir.FullName;
                scName = fullName;
                if (Directory.GetFiles(fullName, exe, SearchOption.AllDirectories) != null)
                {
                    break;
                }
            }

            string? name = fsName ?? scName;
            
            string[] directories = Directory.GetFiles(name, exe, SearchOption.AllDirectories);
            foreach (string file in directories)
            {
              right = file;
            }

            return right;
        }
    }
}

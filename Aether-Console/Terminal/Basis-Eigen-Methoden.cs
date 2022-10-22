﻿
using Aether_Console.Classes_JSON;
using Amazon.Runtime.Internal.Transform;
using LibreTranslate.Net;
using Microsoft.Win32;
using Nancy.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Aether_Console.Terminal
{
    partial class Basis
    {

        public static IDictionary<string, string> applications;
        public static readonly List<char> KEYWORDS = new List<char>(){ '.', '!', '?' };
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

        private static async void translator(string text)
        {
            try
            {
                if (text.Length <= 100)
                {
                    string translation = "";
                    int? counter = 0;
                    while (counter != null)
                    {
                        counter = int.MaxValue;
                        foreach (char c in KEYWORDS)
                        {
                            if (text.Contains(c))
                            {
                                if (text.IndexOf(c) < counter)
                                {
                                    counter = text.IndexOf(c);
                                }
                            }
                        }

                        if (counter != int.MaxValue)
                        {
                            string sentence = text.Substring(0, (int)(counter + 1));
                            string lastCH = text.Substring((int)(counter), 1);
                            if (lastCH != "?")
                            {
                                translation += $"{await LanguageCheck(text, sentence)}";
                            }
                            else
                            {
                                translation += $"{await LanguageCheck(text, sentence)}" + lastCH;
                            }
                            text = text.Substring((int)(counter + 2));
                        }
                        else if (text.Length > 3 && !text.StartsWith("to") && !text.StartsWith("from"))
                        {
                            translation += $"{await LanguageCheck(text)}";
                            counter = null;
                        }
                        else
                        {
                            counter = null;
                        }
                    }
                    Console.WriteLine(translation.Replace("&", " "));
                }
                else
                {
                    Console.WriteLine("Please enter a text less long than 100 words!");
                }
            }catch (ArgumentException ex)
            {
                Console.WriteLine("Sorry, Aether couldn't find the requested languages");
            }
        } 

        private static async Task<string> LanguageCheck(string text, string sentence = "")
        {
            if(sentence == "")
            {
                sentence = text;
            }
            var t1 = getLanguages();
            List<Language> languages = t1.Result;
            Dictionary<string, string> languagesDict = new ();
            for(int i = 0; i < languages.Count; i++)
            {
                languagesDict.Add(languages[i].name, languages[i].code);
            }
            if (text.Contains("from") && text.Contains("to"))
            {
                string lstext = text.Substring(text.LastIndexOf("from") + 5);
                string word1 = lstext.Substring(0, lstext.IndexOf(" "));
                if (lstext.LastIndexOf("to") == -1)
                {
                    return await Translate(sentence);
                }
                string word2 = lstext.Substring(lstext.LastIndexOf("to") + 3);
                if (languagesDict.ContainsKey(word1) && languagesDict.ContainsKey(word2))
                {
                    sentence = sentence.Replace(" ", "&");
                    return await Translate(sentence, languagesDict[word2], languagesDict[word1]);
                }
                else
                {
                    throw new ArgumentException("Sorry, Aether couldn't find the requested languages");
                }

            } else if (text.Contains("to"))
            {
                string word = text.Substring(text.LastIndexOf("to") + 3);
                if (languagesDict.ContainsKey(word))
                {
                    sentence = sentence.Replace(" ", "&");
                    return await Translate(sentence, languagesDict[word]);
                } else
                {
                    throw new ArgumentException("Sorry, Aether couldn't find the requested languages");
                }
            }
            sentence = sentence.Replace(" ", "&");
            return await Translate(sentence);
        }

        private static async Task<List<Language>> getLanguages()
        {
            string url = "https://lingva.ml/api/v1/languages/?:(source|target)";
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Content = new StringContent("", Encoding.UTF8, "application/json"),
            };
            string body = "";
            List<Language> languages;
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
                var js = new JavaScriptSerializer();
                Root2 speaks = js.Deserialize<Root2>(body);
                languages = speaks.languages;
            }
            return languages;
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

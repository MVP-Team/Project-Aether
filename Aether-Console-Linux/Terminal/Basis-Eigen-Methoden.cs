
using Aether_Console.Classes_JSON;
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

        public static readonly List<char> KEYWORDS = new List<char>(){ '.', '!', '?' };
        private static Dictionary<string, string> languages = Languages();
        static Basis()
        {
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
        

        private static Dictionary<string, string> Languages()
        {
            var t1 = getLanguages();
            List<Language> languages = t1.Result;
            Dictionary<string, string> languagesDict = new();
            for (int i = 0; i < languages.Count; i++)
            {
                languagesDict.Add(languages[i].name.ToLower(), languages[i].code);
            }
            return languagesDict;
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
                                if (text.LastIndexOf(c) == text.Length - 1)
                                {
                                    text = text.Substring(0, text.LastIndexOf(c));
                                }
                            }
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
            if (text.Contains("from") && text.Contains("into"))
            {
                string lstext = text.Substring(text.LastIndexOf("from") + 5);
                string word1 = lstext.Substring(0, lstext.IndexOf(" ")).ToLower();
                if (lstext.LastIndexOf("into") == -1)
                {
                    return await Translate(sentence);
                }
                string word2 = lstext.Substring(lstext.LastIndexOf("into") + 5).ToLower();
                if (languages.ContainsKey(word1) && languages.ContainsKey(word2))
                {
                    if (text == sentence)
                    {
                        sentence = sentence.Substring(0, sentence.LastIndexOf("from") - 1);
                    }
                    sentence = sentence.Replace(" ", "&");
                    return await Translate(sentence, languages[word2], languages[word1]);
                }
                else
                {
                    throw new ArgumentException("Sorry, Aether couldn't find the requested languages");
                }

            } else if (text.Contains("into"))
            {
                string word = text.Substring(text.LastIndexOf("into") + 5).ToLower();
                Console.WriteLine(word);
                if (languages.ContainsKey(word))
                {
                    if (text == sentence)
                    {
                        sentence = sentence.Substring(0, sentence.LastIndexOf("into") - 1);
                    }
                    sentence = sentence.Replace(" ", "&");
                    return await Translate(sentence, languages[word]);
                }
                else
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
        
    }
}

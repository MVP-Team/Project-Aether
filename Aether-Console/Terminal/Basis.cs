using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Immutable;
using System.Collections;
using Newtonsoft.Json;
using System.Net.Mime;
using Aether_Console.Classes_JSON;
using Nancy.Json;
using static System.Net.WebRequestMethods;
using System.Reflection;
using NAudio.Wave.SampleProviders;

namespace Aether_Console.Terminal
{
    partial class Basis
    {
        public static readonly List<string> COMMANDS = new() { "open", "search", "translate", "close", "open,", "search,", "translate,", "random"};
        private static List<string> translations = new List<string>();
        public static void Lines()
        {
            string s = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("Aether-Console"));
            
            string? beginning = Console.ReadLine();

            bool ask = false;

            if (beginning == "Aether voice")
            {
                Console.WriteLine("Hey, what do you want to ask for?");
                ask = true;
                while (ask == true)
                {
                    Voice_Recorder vs = new Voice_Recorder();
                    vs.VoiceRecorder();
                }
            } else if (beginning == "Aether")
            {
                Console.WriteLine("Hey, what do you want to ask for?");
                ask = true;
                while (ask == true)
                {
                    ask = Answer();
                }
            }
        }

        public static bool Answer(string? line = null)
        {
            if (line == null) line = Console.ReadLine();
            while(line?.IndexOf(" ") == 0)
            {
                line = line.Substring(1);
            }
            string snLine = (line.Contains(" ")) ? line.Substring(0, line.IndexOf(" ")).ToLower() : line.ToLower();
            if (COMMANDS.Contains(snLine))
            {
                line = line.Substring(line.IndexOf(" ") + 1);
            }
            else
            {
                try
                {
                    Translate(snLine);
                    Thread.Sleep(1600);
                    line = line.Substring(line.IndexOf(" ") + 1);
                    foreach (string s in translations)
                    {
                        if (COMMANDS.Contains(s.ToLower()))
                        {                       
                            snLine = s.ToLower();
                            break;
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Sorry there is no such keyword!");
                }
            }
            snLine = (snLine.Contains(","))? snLine.Substring(0, snLine.IndexOf(",")) : snLine;
            snLine = (snLine.Contains(".")) ? snLine.Substring(0, snLine.IndexOf(".")) : snLine;
            Console.WriteLine(snLine);
            switch (snLine)
            {
                case "open":
                    Console.WriteLine("Application will be opened!");
                    Application(line);
                    break;
                case "search":
                    Console.WriteLine("Search will be opened!");
                    Search(line);
                    break;
                case "translate":
                    Console.WriteLine("Your translated sentence!");
                    translator(line);
                    break;
                case "close":
                    Console.WriteLine("Bye bye, see you later! ;)");
                    Environment.Exit(0);
                    break;
                case "random":
                    GetJoke();
                    break;
                default:
                    Console.WriteLine("Please enter a valid command.");
                    break;
            }
            Lines();
            return true;
        }

        private async static Task<string> Translate(string word, string toLang = "en", string fromLang = "auto")
        {
            string url = "https://lingva.ml/api/v1/" + fromLang + "/"+ toLang + "/" + word;
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Content = new StringContent("", Encoding.UTF8, "application/json"),
            };

            var body = "";
            List<String> endlist;
            string translation;
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
                var js = new JavaScriptSerializer();
                Root translateObject = js.Deserialize<Root>(body);
                translation = translateObject.translation;
                endlist = new List<String>();
                endlist.Add(translation);
                if (translateObject.info.extraTranslations.Count() != 0)
                {
                    var list = translateObject.info.extraTranslations[0].list;
                foreach (var item in list)
                {
                    endlist.Add(item.word);
                }

                }
            }
            translations = endlist;
            return translation;
        }
    }
}




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
using LibreTranslate.Net;
using System.Collections;
using Newtonsoft.Json;
using System.Net.Mime;
using Aether_Console.Classes_JSON;
using Nancy.Json;
using static System.Net.WebRequestMethods;

namespace Aether_Console.Terminal
{
    partial class Basis
    {
        public static readonly List<string> COMMANDS = new() { "open", "search", "translate" };
        private static List<string> translations = new List<string>();
        public void Lines()
        {
            string? beginning = Console.ReadLine();

            bool ask = false;

            if (beginning == "Aether")
            {
                Console.WriteLine("Hey, what do you want to ask for?");
                ask = true;
            }

            while (ask == true)
            {
                ask = Answer();
            }
        }

        private bool Answer()
        {
            string? line = Console.ReadLine();

            line = line.Substring(line.IndexOf(" ") + 1);
            string snLine = (line.Contains(" ")) ? line.Substring(0, line.IndexOf(" ")) : line;
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
                default:
                    Console.WriteLine("Please enter a valid command.");
                    break;
            }

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




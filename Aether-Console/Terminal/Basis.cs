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

namespace Aether_Console.Terminal
{
    partial class Basis
    {
        public static readonly List<string> COMMANDS = new() { "open", "search" };
        private string translatedWord;
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
                    Thread.Sleep(1300);
                    line = line.Substring(line.IndexOf(" ") + 1);
                    snLine = translatedWord.ToLower() ?? "None";
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
                default:
                    Console.WriteLine("Please enter a valid command.");
                    break;
            }

            return true;
        }

        private async void Translate(string word)
        {
            if (word.Length <= 10) {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://microsoft-translator-text.p.rapidapi.com/translate?api-version=3.0&to%5B0%5D=en&textType=plain&profanityAction=NoAction"),
                    Headers =
    {
        { "X-RapidAPI-Key", "7d51783242msh13bdc185ab92a2cp19a885jsn01e62db23b9f" },
        { "X-RapidAPI-Host", "microsoft-translator-text.p.rapidapi.com" },
    },
                    Content = new StringContent("[\r\n    {\r\n        \"Text\": \"" + word + "\"\r\n    }\r\n]")
                    {
                        Headers =
        {
            ContentType = new MediaTypeHeaderValue("application/json")
        }
                    }
                };
                var body = "";
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    body = await response.Content.ReadAsStringAsync();
                    body = body.Substring(body.IndexOf("text\":\"") + 7, body.IndexOf(",\"to\"") - 8 - body.IndexOf("text\":\""));
                    translatedWord = body;
                }
            }
            else
            {
                throw new ArgumentException("Length is not ok");
            }
        }
    }
}




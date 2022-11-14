// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using Nancy.Json;
using Newtonsoft.Json;
using Aether_Console.Classes_JSON;
using System.Text;
using Aether_Console.Terminal;

/*
Console.OutputEncoding = Console.InputEncoding = Encoding.Unicode;
Basis bas = new Basis();
bas.Lines();
*/

using System;
using System.Speech.Recognition;

namespace SpeechRecognitionApp
{
    class Program
    {
        static void Main(string[] args)
        {

            // Create an in-process speech recognizer for the en-US locale.  
            using (
            SpeechRecognitionEngine recognizer =
              new SpeechRecognitionEngine(
                new System.Globalization.CultureInfo("en-US")))
            {

                // Create and load a dictation grammar.  
                recognizer.LoadGrammar(new DictationGrammar());

                // Add a handler for the speech recognized event.  
                recognizer.SpeechRecognized +=
                  new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

                // Configure input to the speech recognizer.  
                recognizer.SetInputToDefaultAudioDevice();

                // Start asynchronous, continuous speech recognition.  
                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                // Keep the console window open.  
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }

        // Handle the SpeechRecognized event.  
        static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Recognized text: " + e.Result.Text);
        }
    }
}



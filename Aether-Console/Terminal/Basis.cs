using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aether_Console.Terminal
{
    public enum Writings
    {
        Open = 0
    }
    partial class Basis
    {

        public static void Lines()
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

        private static bool Answer()
        {
            string? line = Console.ReadLine();

            line = line.Substring(line.IndexOf(" ") + 1);
            string snLine = (line.Contains(" ")) ? line.Substring(0, line.IndexOf(" ")) : line;
            line = line.Substring(line.IndexOf(" ") + 1);
            string lsLine = (line.Contains(" ")) ? line.Substring(0, line.IndexOf(" ")) : line;


            switch (snLine)
            {
                case "open":
                    Console.WriteLine("Application will be opened!");
                    Application(lsLine);
                    break;
                case "play":
                    Console.WriteLine("Song will be played!");
                    break;
                default:
                    Console.WriteLine("Please enter a valid command.");
                    break;
            }
            
            return true;
        }
            
    }
}




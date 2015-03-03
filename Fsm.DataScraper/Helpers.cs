using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fsm.DataScraper
{
    public class Helpers
    {
        public static int GetInt(string prompt)
        {
            WritePrompt(prompt);
            var result = int.Parse(Console.ReadLine());
            Console.WriteLine();
            return result;
        }

        public static ConsoleKey GetKey(string prompt)
        {
            WritePrompt(prompt);
            var result = Console.ReadKey();
            Console.WriteLine();
            return result.Key;
        }

        private static void WritePrompt(string prompt)
        {
            Console.WriteLine();
            Console.Write(prompt);
        }

        public static string XmlPath 
        {
            get
            {
                return XmlPath;
            }
        }
    }
}

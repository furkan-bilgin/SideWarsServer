using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Utils
{
    public class Logger
    {
        private static string GetDateString()
        {
            return $"[{DateTime.Now.ToString()}]";
        }

        public static void Info(string text)
        {
            Console.WriteLine($"{GetDateString()} [Info] {text}");
        }

        public static void Error(Exception ex)
        {
            Error(ex.ToString());
        }

        public static void Error(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{GetDateString()} [Error] {text}");
            Console.ResetColor();
        }
    }
}

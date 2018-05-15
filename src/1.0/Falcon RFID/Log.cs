using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEV.Falcon.RFID
{
    public class Log
    {
        public static void Info(string s, params object[] args)
        {
            Console.Write("[{0}]", DateTime.Now.ToString("HH:mm:ss"));
            ConsoleColor cc = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[ INFO  ] ");
            Console.ForegroundColor = cc;
            Console.WriteLine(String.Format(s, args));
        }

        public static void Success(string s, params object[] args)
        {
            Console.Write("[{0}]", DateTime.Now.ToString("HH:mm:ss"));
            ConsoleColor cc = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[SUCCESS] ");
            Console.ForegroundColor = cc;
            Console.WriteLine(String.Format(s, args));
        }

        public static void Alert(string s, params object[] args)
        {
            Console.Write("[{0}]", DateTime.Now.ToString("HH:mm:ss"));
            ConsoleColor cc = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[ ALERT ] ");
            Console.ForegroundColor = cc;
            Console.WriteLine(String.Format(s, args));
        }
    }
}

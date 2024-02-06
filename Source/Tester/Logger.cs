using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    public class Logger
    {
        public static bool enabled = false;
        public static void Log(string? message) {
            if (!enabled) return;
            Console.WriteLine(message);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester;
struct Args
{
    public string Command { get; set; }
    public int Games { get; set; }

    public static Args Parse(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No program provided to run");
            Environment.Exit(1);
        }

        var games = Int32.Parse((args.Length >= 2 ? args[1] : "200"));
        var command = args[0];
        return new Args(command, games);
    }

    private Args(string command, int games)
    {
        this.Command = command;
        this.Games = games;
    }
}
namespace Tester;
struct Args
{
    public string Command { get; set; }
    public int Games { get; set; }
    public bool Silent { get; set; }

    public static Args Parse(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No program provided to run");
            Environment.Exit(1);
        }

        var games = Int32.Parse((args.Length >= 2 ? args[1] : "200"));
        var command = args[0];
        var silent = args.Length >= 3 ? args[2] == "true" : false;
        return new Args(command, games, silent);
    }

    private Args(string command, int games, bool silent)
    {
        this.Command = command;
        this.Games = games;
        this.Silent = silent;
    }
}
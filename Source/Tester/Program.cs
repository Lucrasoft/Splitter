using System.Diagnostics;
using System.Text;
using Tester;

var points = 0;
var i = 0;
var opts = Args.Parse(args);

Logger.enabled = !opts.Silent;

var watch = Stopwatch.StartNew();

var printEnding = () =>
{
    watch.Stop();
    Console.WriteLine($"Games played {i}");
    Console.WriteLine($"Points gotten {points}");
    Console.WriteLine($"Points per game (avg) {points / i}");
    Console.WriteLine($"Time taken {watch.ElapsedMilliseconds / 1000}S");
};

Console.CancelKeyPress += (sender, eventArgs) =>
{
    printEnding();
};


for (; i < opts.Games; i++)
{
    points += await PlayAsync(new Grid(Grids.GridA), opts.Command);
}

printEnding();

static async Task<int> PlayAsync(Grid grid, string command)
{
    var game = new Game(grid);

    var currentDirectory = Environment.CurrentDirectory.Replace("\\Tester\\bin\\Debug\\net8.0", "");

    var process = new Process();
    var startInfo = new ProcessStartInfo
    {
        WindowStyle = ProcessWindowStyle.Hidden,
        FileName = IsWindows() ? "cmd.exe" : "/bin/bash",
        Arguments = IsWindows() ? $"/C {command}" : $"-c \"{command}\"",
        UseShellExecute = false,
        WorkingDirectory = currentDirectory,
        RedirectStandardOutput = true,
        RedirectStandardInput = true,
    };

    process.StartInfo = startInfo;

    var tcs = new TaskCompletionSource<int>();

    process.OutputDataReceived += (sender, args) =>
    {
        Logger.Log(args.Data);
        var data = args.Data;

        if (data == null || !data.StartsWith("p"))
        {
            return;
        }

        var items = data.Split(" ");
        var choice = Int32.Parse(items[1]);
        var location = items[2].Split(",").Select(c => Int32.Parse(c)).ToArray();
        var point = new Point(location[0], location[1]);
        try
        {
            game.Place(point, choice);

            if (game.Rounds == 0)
            {
                var points = game.GetPoints();
                Logger.Log(Print2dMatrix(game.State));
                try
                {
                    process.Close();
                }
                catch (Exception e)
                {
                    //pass
                }
                try
                {
                    process.Kill();

                }
                catch (Exception e)
                {
                    //pass
                }
                tcs.SetResult(points);
                return;
            }

            game.CurrentDice = Game.RollDices();
            process.StandardInput.WriteLine($"d {game.CurrentDice.Item1} {game.CurrentDice.Item2}");
        }
        catch (Exception e)
        {
            Logger.Log(e.Message);
        }
    };

    process.Start();

    process.BeginOutputReadLine();
    process.StandardInput.WriteLine($"{grid.Width()} {grid.Height()} {game.Rounds}");

    foreach (var line in Print2dMatrix(grid._grid).TrimEnd().Split("\n"))
    {
        process.StandardInput.WriteLine(line);
    }

    process.StandardInput.WriteLine($"d {game.CurrentDice.Item1} {game.CurrentDice.Item2}");

    return await tcs.Task;
}



static bool IsWindows()
{
    return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);
}

static string Print2dMatrix(int[,] m)
{
    var res = new StringBuilder();
    for (int i = 0; i < m.GetLength(0); i++)
    {
        string rowString = string.Join(" ", Enumerable.Range(0, m.GetLength(1)).Select(j => m[i, j]));
        res.Append(rowString + "\n");
    }
    return res.ToString();
}

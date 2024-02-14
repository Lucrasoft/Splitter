using System.Diagnostics;
using System.Text;

namespace Tester;
class Program
{
    /// <summary>
    ///     Try your splitter bot with the tester program
    /// </summary>
    /// <param name="games">Amount of games to play before spitting out the results</param>
    /// <param name="silent">Makes it so all output is hidden to speed up the program</param>
    /// <param name="seed">The random seed to use this will make it so you can retry the same rolls</param>
    /// <param name="layout">Layout to use there's 2, 1 is without the hearts 2 is with the hearts</param>
    /// <param name="command">Command to execute</param>
    /// <returns></returns>
    static async Task<int> Main(int games = 200, bool silent = false, string? seed = null, int layout = 1, string[] command = null)
    {

        var points = 0;
        var i = 0;

        Logger.enabled = !silent;

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

        for (; i < games; i++)
        {
            if (seed is not null) Game.RND = new Random(CustomHash(seed) + i);
            points += await PlayAsync(new Grid(layout == 1 ? Grids.GridA : Grids.GridB), string.Join(" ", command));
        }

        printEnding();

        return 0;
    }
    /// <summary>
    ///     A common hash implementation
    ///     Useful to be able to get a determined output
    /// </summary>
    /// <param name="input">Strng to "hash"</param>
    /// <returns></returns>
    public static int CustomHash(string input)
    {
        int hash = 0;

        foreach (char c in input)
        {
            hash = (hash * 31) + c;
        }

        return hash;
    }

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

            if (data == null || data.StartsWith("#"))
            {
                return;
            }



            var items = data.Replace(",", " ").Split(" ");
            int choice = int.TryParse(items[0], out choice) ? choice : -1;
            if (choice == -1)
            {
                Logger.Log("Input not a number skipping!");
                return;
            }
            var point = new Point(Int32.Parse(items[1]), Int32.Parse(items[2]));
            try
            {
                game.Place(point, choice);

                if (game.Rounds == 0)
                {
                    var points = game.GetPoints();
                    Logger.Log(Print2dMatrix(game.State));
                    try
                    {
                        process.Kill();
                        process.Close();
                        process.Dispose();
                    }
                    catch (Exception)
                    {
                        //pass
                    }
                    tcs.SetResult(points);
                    return;
                }

                game.CurrentDice = Game.RollDices();
                process.StandardInput.WriteLine($"{game.CurrentDice.Item1} {game.CurrentDice.Item2}");
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

        process.StandardInput.WriteLine($"{game.CurrentDice.Item1} {game.CurrentDice.Item2}");

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


}

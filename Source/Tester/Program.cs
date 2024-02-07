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
    int[,] state = new int[grid.Height(), grid.Width()];

    var rounds = CalculateRounds(grid);

    var currentDirectory = Environment.CurrentDirectory.Replace("\\Tester\\bin\\Debug\\net8.0", "");

    var process = new Process();
    var startInfo = new ProcessStartInfo
    {
        WindowStyle = ProcessWindowStyle.Hidden,
        FileName = "cmd.exe",
        Arguments = $"/C {command}",
        UseShellExecute = false,
        WorkingDirectory = currentDirectory,
        RedirectStandardOutput = true,
        RedirectStandardInput = true,
    };

    process.StartInfo = startInfo;

    var currentDice = RNG.RollDices();


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

        if (choice != currentDice.Item1 && choice != currentDice.Item2)
        {
            Logger.Log($"{choice} was not rolled please try again!");
            return;
        }

        if (grid.Get(point) == Grids.EMPTY)
        {
            Logger.Log($"Cant place on invalid tile it non placeable {location[0]},{location[1]}");
            return;
        }


        if (state[point.y, point.x] != Grids.EMPTY)
        {
            Logger.Log($"Cant place on invalid tile it already has something {location[0]},{location[1]}");
            return;
        }

        rounds -= 1;

        state[point.y, point.x] = choice;
        state[point.y, grid.Width() - point.x - 1] = choice == currentDice.Item1 ? currentDice.Item2 : currentDice.Item1;

        if (rounds == 0)
        {
            var points = Points.CalculatePoints(grid, state);
            Logger.Log(Print2dMatrix(state));
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

        currentDice = RNG.RollDices();

        process.StandardInput.WriteLine($"d {currentDice.Item1} {currentDice.Item2}");
    };

    process.Start();

    process.BeginOutputReadLine();

    process.StandardInput.WriteLine($"{grid.Width()} {grid.Height()} {rounds}");

    foreach (var line in Print2dMatrix(grid._grid).TrimEnd().Split("\n"))
    {
        process.StandardInput.WriteLine(line);
    }

    process.StandardInput.WriteLine($"d {currentDice.Item1} {currentDice.Item2}");

    return await tcs.Task;
}

static int CalculateRounds(Grid grid)
{
    int rounds = 0;

    foreach (var point in grid)
    {
        if (grid.Get(point) != Grids.EMPTY)
        {
            rounds++;
        }
    }

    rounds /= 2;

    return rounds;
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

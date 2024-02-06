using System.Diagnostics;
using System.Text;

int[,] grid = {
    {0, 0, 1, 1, 1, 1, 0, 0},
    {0, 1, 1, 1, 1, 1, 1, 0},
    {1, 1, 1, 1, 1, 1, 1, 1},
    {2, 1, 1, 1, 1, 1, 1, 2},
    {1, 1, 1, 1, 1, 1, 1, 1},
    {0, 1, 1, 1, 1, 1, 1, 0},
    {0, 0, 1, 1, 1, 1, 0, 0},
};
var points = 0;
var i = 0; 
var opts = Args.Parse(args);

Console.CancelKeyPress += delegate {
    Console.WriteLine($"Games played {i}");
    Console.WriteLine($"Points gotten {points}");
    Console.WriteLine($"Points per game (avg) {points / i}");
};

for (; i < opts.Games; i++)
{
    points += await PlayAsync(grid, opts.Command);
}

static async Task<int> PlayAsync(int[,] grid, string command)
{
    int[,] state = new int[grid.GetLength(0), grid.GetLength(1)];

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

    var currentDice = RollDice();


    var tcs = new TaskCompletionSource<int>();

    process.OutputDataReceived += (sender, args) =>
    {
        Console.WriteLine(args.Data);
        var data = args.Data;

        if (data == null || !data.StartsWith("p"))
        {
            return;
        }

        var items = data.Split(" ");
        var choice = Int32.Parse(items[1]);
        var location = items[2].Split(",").Select(c => Int32.Parse(c)).ToArray();

        if (choice != currentDice.Item1 && choice != currentDice.Item2)
        {
            Console.WriteLine($"{choice} was not rolled please try again!");
            return;
        }

        rounds -= 1;

        state[location[1], location[0]] = choice;
        state[location[1], grid.GetLength(1) - location[0] - 1] = choice == currentDice.Item1 ? currentDice.Item2 : currentDice.Item1;

        if (rounds == 0)
        {
            var points = CalculatePoints(state, grid);
            Console.WriteLine(Print2dMatrix(state));
            try
            {
                process.Close();
            }catch(Exception e)
            {
                //pass
            }
            try
            {
                process.Kill();

            }catch(Exception e )
            {

            }


            tcs.SetResult(points);
            return;
        }

        currentDice = RollDice();

        process.StandardInput.WriteLine($"d {currentDice.Item1} {currentDice.Item2}");
    };

    process.Start();
    //await process.StandardOutput.ReadToEndAsync();

    process.BeginOutputReadLine();

    process.StandardInput.WriteLine($"{grid.GetLength(0)} {grid.GetLength(1)} {rounds}");

    foreach (var line in Print2dMatrix(grid).TrimEnd().Split("\n"))
    {
        process.StandardInput.WriteLine(line);
    }

    process.StandardInput.WriteLine($"d {currentDice.Item1} {currentDice.Item2}");

    return await tcs.Task;
}

static int CalculatePoints(int[,] state, int[,] grid)
{
    var points = 0;
    for (var i = 1; i <= 6; i++)
    {
        var newGrid = (int[,])state.Clone();
        for (int k = 0; k < newGrid.GetLength(0); k++)
        {
            for (int j = 0; j < newGrid.GetLength(1); j++)
            {
                if (newGrid[k, j] != i) // If the value is not i, replace it with 0
                {
                    newGrid[k, j] = 0;
                }
                else
                {
                    newGrid[k, j] = 1;
                }
            }
        }

        var res = Tester.Matrix.CountIslands(newGrid);
        points += res.Where(c => c.Length == i).Count() * i;

        List<Tuple<int, int>> pointPlaces = new List<Tuple<int, int>>();

        for (int x = 0; x < grid.GetLength(0); x += 1)
        {
            for (int y = 0; y < grid.GetLength(1); y += 1)
            {
                if (grid[x, y] == 2)
                {
                    pointPlaces.Add(new Tuple<int, int>(
                        x, y
                        ));
                }



            }
        }

        foreach (var arr in res.Where(c => c.Length == i))
        {
            if (arr.Any(c =>
               pointPlaces.Any(p => p.Equals(c))))
            {
                points += i;
                Console.WriteLine("BONUS POINTS");
            }
        }

    }
    return points;
}

static (int, int) RollDice()
{
    Random rnd = new Random();

    return (
        rnd.Next(1, 7),
        rnd.Next(1, 7)
    );
}

static int CalculateRounds(int[,] grid)
{
    int rounds = 0;

    for (int i = 0; i < grid.GetLength(0); i++)
    {
        for (int j = 0; j < grid.GetLength(1); j++)
        {
            if (grid[i, j] != 0)
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
        
        var games = Int32.Parse((args.Length >= 2 ? args[1] : "1000"));
        var command = args[0];
        return new Args(command, games);
    }

    private Args(string command,  int games)
    {
        this.Command = command;
        this.Games = games;
    }
}
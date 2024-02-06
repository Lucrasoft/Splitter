// See https://aka.ms/new-console-template for more information

int[,] grid = {
    {0, 0, 1, 1, 1, 1, 0, 0},
    {0, 1, 1, 1, 1, 1, 1, 0},
    {1, 1, 1, 1, 1, 1, 1, 1},
    {2, 1, 1, 1, 1, 1, 1, 2},
    {1, 1, 1, 1, 1, 1, 1, 1},
    {0, 1, 1, 1, 1, 1, 1, 0},
    {0, 0, 1, 1, 1, 1, 0, 0},
};

var games = Int32.Parse((args.Length >= 2 ? args[1] : "1000"));

int[,] state = new int[grid.GetLength(0), grid.GetLength(1)];
int rounds = 0;

for (int i = 0; i < grid.GetLength(0); i++)
{
    for (int j = 0; j < grid.GetLength(1); j++)
    {
        state[i, j] = 0;
        if (grid[i, j] != 0)
            rounds++;
    }
}

rounds /= 2;

Console.WriteLine($"Playing {rounds} Rounds");

Console.WriteLine(Environment.CurrentDirectory);

var currentDice = (2, 4);

Random rnd = new Random();

System.Diagnostics.Process process = new System.Diagnostics.Process();
System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
startInfo.WorkingDirectory = Environment.CurrentDirectory.Replace("\\Tester\\bin\\Debug\\net8.0", "");
startInfo.FileName = "cmd.exe";
startInfo.Arguments = $"/C {args[0]}";
process.StartInfo = startInfo;
process.StartInfo.UseShellExecute = false;
process.StartInfo.RedirectStandardOutput = true;
process.StartInfo.RedirectStandardInput = true;
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
        Console.WriteLine("Done!!!");
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
            points += res.Where(c => c.Length == i).Count();

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
                    points += 1;
                    Console.WriteLine("BONUS POINTS");
                }
            }

        }
        for (int i = 0; i < state.GetLength(0); i++)
        {
            string rowString = string.Join(" ", Enumerable.Range(0, state.GetLength(1)).Select(j => state[i, j]));
            Console.WriteLine(rowString);
        }
        Console.WriteLine($"Received points {points}");
        //DIE!
        process.Kill();
        Environment.Exit(1);

    }

    currentDice = (
        rnd.Next(1, 7),
        rnd.Next(1, 7)
    );

    process.StandardInput.WriteLine($"d {currentDice.Item1} {currentDice.Item2}");
};
process.Start();
process.BeginOutputReadLine();

process.StandardInput.WriteLine($"{grid.GetLength(0)} {grid.GetLength(1)}");
for (int i = 0; i < grid.GetLength(0); i++)
{
    string rowString = string.Join(" ", Enumerable.Range(0, grid.GetLength(1)).Select(j => grid[i, j]));
    process.StandardInput.WriteLine(rowString);
}

process.StandardInput.WriteLine($"d {currentDice.Item1} {currentDice.Item2}");

// This places 2 on 2,2 and also makes it so 4 is placed on 7,2
// input message example p 2 2,2

await process.WaitForExitAsync();


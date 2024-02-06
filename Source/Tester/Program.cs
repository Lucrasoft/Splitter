// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int[,] grid = {
    {0, 0, 1, 1, 1, 1, 0, 0},
    {0, 1, 1, 1, 1, 1, 1, 0},
    {1, 1, 1, 1, 1, 1, 1, 1},
    {2, 1, 1, 1, 1, 1, 1, 2},
    {1, 1, 1, 1, 1, 1, 1, 1},
    {0, 1, 1, 1, 1, 1, 1, 0},
    {0, 0, 1, 1, 1, 1, 0, 0},
};


int[,] state = new int[grid.GetLength(0), grid.GetLength(1)];
int games = 0;

for (int i = 0; i < grid.GetLength(0); i++)
{
    for (int j = 0; j < grid.GetLength(1); j++)
    {
        state[i, j] = 0;
        if (grid[i, j] != 0)
            games++;
    }
}

games /= 2;

Console.WriteLine($"Playing {games} Games");

Console.WriteLine(Environment.CurrentDirectory);

var currentDice = (2, 4);

Random rnd = new Random();

System.Diagnostics.Process process = new System.Diagnostics.Process();
System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
startInfo.WorkingDirectory = Environment.CurrentDirectory.Replace("\\Tester\\bin\\Debug\\net8.0", "");
startInfo.FileName = "cmd.exe";
startInfo.Arguments = "/C dotnet run --project Example";
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
        //Console.WriteLine($"{data}");
        //return early
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

    games -= 1;


    state[location[1], location[0]] = choice;
    state[location[1], grid.GetLength(1) - location[0] - 1] = choice == currentDice.Item1 ? currentDice.Item2 : currentDice.Item1;


    if (games == 0)
    {
        Console.WriteLine("Done!!!");
        //foreach (var row in state)
        //{
        //    Console.WriteLine(String.Join(" ", row));
        //}
        // calculate points

        var points = 0;
        //var res = GetConnectedSets(state);

        //for (var i = 1; i <= 6; i++)
        //{
        //    Console.WriteLine($"Points {GetConnectedSets(state, i)}x {i}");
        //}

        for (var i = 1; i <= 6; i++)
        {
            int[,] newGrid = state.Clone() as int[,];
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
            var res = Tester.Matrix.countIslands(newGrid);
            points += res.Where(c => c == i).Count();
        }
        Console.WriteLine($"Received points {points}");
        //DIE!
        process.Kill();
        Environment.Exit(1);

    }

    //foreach (var row in state)
    //{
    //    Console.WriteLine(String.Join(" ", row));
    //}

    currentDice = (
        rnd.Next(1, 6),
        rnd.Next(1, 6)
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


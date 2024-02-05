// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int[][] grid = [
    [0, 0, 1, 1, 1, 1, 0, 0],
    [0, 1, 1, 1, 1, 1, 1, 0],
    [1, 1, 1, 1, 1, 1, 1, 1],
    [2, 1, 1, 1, 1, 1, 1, 2],
    [1, 1, 1, 1, 1, 1, 1, 1],
    [0, 1, 1, 1, 1, 1, 1, 0],
    [0, 0, 1, 1, 1, 1, 0, 0],
];


// im sure theres a simpler way
int[][] state = grid.Select((r) => r.Select(f => 0).ToArray()).ToArray();
int games = grid.Select(f => f.Count(c => c != 0)).Sum() / 2;

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

    if (!data.StartsWith("p"))
    {
        Console.WriteLine($"Malformed data received {data}");
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


    state[location[1]][location[0]] = choice;
    state[location[1]][grid[0].Length - location[0] - 1] = choice == currentDice.Item1 ? currentDice.Item2 : currentDice.Item1;

    var done = !state.Select(f => f.FirstOrDefault(f => f == 0, -1)).Contains(0);
    
    if (games == 0)
    {
        Console.WriteLine("Done!!!");
        foreach (var row in state)
        {
            Console.WriteLine(String.Join(" ", row));
        }
        // calculate points
        


        //DIE!
        process.Kill();
        Environment.Exit(1);

    }

    foreach (var row in state)
    {
        Console.WriteLine(String.Join(" ", row));
    }

    currentDice = (
        rnd.Next(1, 6),
        rnd.Next(1, 6)
    );

    process.StandardInput.WriteLine($"d {currentDice.Item1} {currentDice.Item2}");
};
process.Start();
process.BeginOutputReadLine();

process.StandardInput.WriteLine($"{grid.Length} {grid[0].Length}");
foreach (var row in grid)
{
    process.StandardInput.WriteLine(String.Join(" ", row));
}

process.StandardInput.WriteLine($"d {currentDice.Item1} {currentDice.Item2}");


// This places 2 on 2,2 and also makes it so 4 is placed on 7,2
// input message example p 2 2,2

await process.WaitForExitAsync();


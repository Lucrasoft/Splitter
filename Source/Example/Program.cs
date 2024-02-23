// See https://aka.ms/new-console-template for more information
// We read the first line containing the sizes & rounds
var line1 = Console.ReadLine();
// log it to the console for debugging-purposes
Console.WriteLine("# " + line1);
var sizes = line1.Split(" ");
var width = Int32.Parse(sizes[0]);
var height = Int32.Parse(sizes[1]);
var rounds = Int32.Parse(sizes[2]);

var gameState = new List<int[]>();
// loop over the entire grid to get its sizes
for (int i = 0; i < height; i++)
{
    var line = Console.ReadLine();
    // log it to the console for debugging-purposes
    Console.WriteLine("# " + line);
    // Fill the grid with zeros indicating that that slot can be used to roll on and the rest will be filled with -1 (non placeble)
    var data = line!.Split(" ").Select(c => Int32.Parse(c)).Select(c => c != 0 ? 0 : -1).ToArray();
    gameState.Add(data);
}

var rnd = new Random();

for (int i = 0; i < rounds; i++)
{
    // We have to wait a bit Console.Readline() doesn't have a function that waits till a line is there it goes either theres a line or null
    Thread.Sleep(10);
    var roll = Console.ReadLine();
    if (roll == null) continue;
    // log it to the console for debugging-purposes
    Console.WriteLine("# " + roll);

    var nums = roll.Split(" ");
    var first = Int32.Parse(nums[0]);
    // we dont actually use the second roll
    var second = Int32.Parse(nums[1]);

    var x = rnd.Next(0, width / 2);
    var y = rnd.Next(0, height);
    // set a hard limit
    var limit = 5000;

    // finds the next valid place to place the roll this is a brute-forcing way.
    // if its not 0 its not a valid spot
    while (gameState[y][x] != 0 && limit != 0)
    {
        limit -= 1;
        // we only want to place on the first half of the board
        // the other half is ignored
        x = rnd.Next(0, width / 2);
        y = rnd.Next(0, height);
    }
    gameState[y][x] = first;
    // send the result to tester
    Console.WriteLine($"{first} {x} {y}");
}

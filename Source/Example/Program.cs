// See https://aka.ms/new-console-template for more information
var line1 = Console.ReadLine();
Console.WriteLine("# " + line1);
var sizes = line1.Split(" ");
var width = Int32.Parse(sizes[0]);
var height = Int32.Parse(sizes[1]);
var rounds = Int32.Parse(sizes[2]);

var arr = new List<int[]>();

for (int i = 0; i < height; i++)
{
    var line = Console.ReadLine();
    Console.WriteLine("# " + line);
    // Fill the grid with zeros indicating that that slot can be used to roll on and the rest will be filled with -1 (non placeble)
    var data = line!.Split(" ").Select(c => Int32.Parse(c)).Select(c => c != 0 ? 0 : -1).ToArray();
    arr.Add(data);
}

var rnd = new Random();

for (int i = 0; i < rounds; i++)
{
    Thread.Sleep(10);
    var roll = Console.ReadLine();
    if (roll == null) continue;
    Console.WriteLine("# " + roll);

    var nums = roll.Split(" ");
    var first = Int32.Parse(nums[0]);
    var second = Int32.Parse(nums[1]);

    var x = rnd.Next(0, width / 2);
    var y = rnd.Next(0, height);
    var limit = 5000;
    while ((arr[y][x] != 0 || arr[y][x] == -1) && limit != 0)
    {
        limit -= 1;
        x = rnd.Next(0, width / 2);
        y = rnd.Next(0, height);
    }
    arr[y][x] = first;
    Console.WriteLine($"{first} {x} {y}");
}

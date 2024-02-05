// See https://aka.ms/new-console-template for more information
var line1 = Console.ReadLine();
Console.WriteLine(line1);
var sizes = line1.Split(" ");
var height = Int32.Parse(sizes[0]);
var width = Int32.Parse(sizes[1]);

var arr = new List<int[]>();

for (int i = 0; i < height; i++)
{
   var line = Console.ReadLine();
   Console.WriteLine(line);
   var data = line!.Split(" ").Select(c=> Int32.Parse(c)).Select(c => c != 0 ? 0 : -1).ToArray();
   arr.Add(data);
}

var rnd = new Random();
while(true)
{

    Thread.Sleep(10);
    var roll = Console.ReadLine();
    if (roll == null) continue;
    Console.WriteLine(roll);
    
    var nums = roll.Split(" ");
    var first = Int32.Parse(nums[1]);
    var second = Int32.Parse(nums[2]);

    var x = rnd.Next(0, width / 2);
    var y = rnd.Next(0, height);
    while (arr[y][x] != 0 || arr[y][x] == -1)
    {
        x = rnd.Next(0, width / 2);
        y = rnd.Next(0, height);
    }
    
    arr[y][x] = first;
    Console.WriteLine($"p {first} {x},{y}");
}

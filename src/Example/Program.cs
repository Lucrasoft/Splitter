// In this competition these values are fixed.. 
int width = 8;
int height = 7;
int rounds = 22;

// As a bot, you read the first line which contains the sizes & rounds, again for completeness.
var firstLine = Console.ReadLine()?.Split(" ");
if (firstLine != null)
{
    width = Int32.Parse(firstLine[0]);
    height = Int32.Parse(firstLine[1]);
    rounds = Int32.Parse(firstLine[2]);
}

// The second thing is to process the layout of the board we receive. 
// There are two different layouts in the game. 
var layout = new int[width, height];

for (int y = 0; y < height; y++)
{
    var line = Console.ReadLine();
    var data = line!.Split(" ").Select(c => Int32.Parse(c)).ToArray();
    for (int x = 0; x < width; x++)
    {
        layout[x, y] = data[x];
    }
}


// The state will hold the local information about which dices are placed on the board.
var state = new int[width, height];

// A helper function to check if a field on the board is free
bool IsFree(int x, int y)
{
    //the layout indicates if a field is available to place a dice on
    //the state indicates if a field is already occupied
    return layout[x,y]>0 && state[x, y] == 0;
}

// A function to get all free positions on the board
List<(int x,int y)> GetFreePositions()
{
    var result = new List<(int x, int y)>();
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (IsFree(x, y))
            {
                result.Add((x, y));
            }
        }
    }
    return result;
}


var rnd = new Random();

for (int i = 0; i < rounds; i++)
{
    // We have to wait a bit Console.Readline()
    // Depending on your platform and mode of execution it not always waits till a line is there it goes either theres a line or null
    Thread.Sleep(10);
    var diceLine = Console.ReadLine();
    if (diceLine != null)
    {
        //We receive the dices from the game engine.
        var dices = diceLine.Split(" ");

        var dice1 = Int32.Parse(dices[0]);
        var dice2 = Int32.Parse(dices[1]);

        //We need to find a free position on the board to place the dices.
        var freePositions = GetFreePositions();
        //We pick a random free position.
        var (x, y) = freePositions[rnd.Next(freePositions.Count)];

        //We keep a record where we placed the dices.
        state[x, y] = dice1;
        //We need to place the second dice on the opposite side of the board.
        state[width - x - 1, y] = dice2;

        //We need to communicate our move to the game engine. 
        //Only one dice is communicated, the second one is calculated by the game engine.
        Console.WriteLine($"{dice1} {x} {y}");
    }
   


}

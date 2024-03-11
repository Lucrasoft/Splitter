namespace Tester;

public class Grids
{
    public const int EMPTY = 0;
    public const int FIELD = 1;
    public const int STAR = 2;
    public const int HEART = 3;

    public static int[,] GridA = {
        {0, 0, 1, 1, 1, 1, 0, 0},
        {0, 1, 1, 1, 1, 1, 1, 0},
        {1, 1, 1, 1, 1, 1, 1, 1},
        {2, 1, 1, 1, 1, 1, 1, 2},
        {1, 1, 1, 1, 1, 1, 1, 1},
        {0, 1, 1, 1, 1, 1, 1, 0},
        {0, 0, 1, 1, 1, 1, 0, 0},
    };
    public static int[,] GridB =
    {
            {1,1,1,0,0,1,3,1 },
            {1,1,1,1,1,1,1,1 },
            {0,3,1,1,1,1,1,0 },
            {0,1,1,0,0,1,1,0 },
            {0,1,1,1,3,1,1,0 },
            {1,1,1,1,1,1,1,1 },
            {1,2,1,0,0,1,2,1 },
    };
}

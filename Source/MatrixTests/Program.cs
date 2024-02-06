int[,] grid = {
    {0, 0, 1, 3, 3, 3, 0, 0},
    {0, 4, 2, 2, 4, 5, 3, 0},
    {3, 4, 5, 2, 4, 5, 2, 1},
    {5, 2, 3, 4, 4, 2, 5, 4},
    {2, 2, 1, 3, 2, 4, 3, 4},
    {0, 5, 1, 3, 5, 5, 2, 0},
    {0, 0, 5, 3, 5, 1, 0, 0}
};

for (var i = 1; i <= 6; i++)
{
    int[,] newGrid = grid.Clone() as int[,];
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
    Console.WriteLine($"{i} island count {res.Length} points {res.Where(c => c.Length == i).Count()}");
}


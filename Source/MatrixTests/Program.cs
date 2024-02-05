int[][] grid = [
    [0, 0, 1, 3, 3, 3, 0, 0],
    [0, 4, 2, 2, 4, 5, 3, 0],
    [3, 4, 5, 2, 4, 5, 2, 1],
    [5, 2, 3, 4, 4, 2, 5, 4],
    [2, 2, 1, 3, 2, 4, 3, 4],
    [0, 5, 1, 3, 5, 5, 2, 0],
    [0, 0, 5, 3, 5, 1, 0, 0]
];
var res = Tester.Matrix.GetConnectedSets(grid);

Console.WriteLine(res);
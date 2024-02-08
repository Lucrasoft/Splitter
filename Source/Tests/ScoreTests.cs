using Tester;

namespace Tests;

public class ScoreTests
{
    public static readonly int[,] GridC =
        {
            { 0,0,0,0,0 },
            { 1,1,1,1,1 },
            { 1,1,1,1,1 },
            { 0,0,0,0,0 },
        };
    public static readonly int[,] GridD =
        {
            { 2 }
        };
    public static readonly int[,] GridE =
    {
        {0,1,1,1,1,0 },
        {3,1,1,3,1,1 },
        {0,1,1,1,1,0 },
    };
    public static readonly int[,] GridF =
    {
       {3,3},
       {1,1},
       {2,2}
    };

    private static int CalculatePoints(int[,] grid, int[,] state)
    {
        return Points.CalculatePoints(new Grid(grid), state);
    }

    [Fact]
    public void TestAllGroups()
    {

        int[,] state =
        {
            { 0,0,0,0,0 },
            { 3,3,3,4,4 },
            { 1,2,2,4,4 },
            { 0,0,0,0,0 },
        };

        Assert.Equal(10, CalculatePoints(GridC, state));
    }


    [Fact]
    public void TestZero()
    {

        int[,] state =
        {
            { 0,0,0,0,0 },
            { 3,2,3,5,4 },
            { 1,1,2,4,4 },
            { 0,0,0,0,0 },
        };

        Assert.Equal(0, CalculatePoints(GridC, state));
    }

    [Fact]
    public void TestDouble()
    {

        int[,] state =
        {
            { 1 }
        };

        Assert.Equal(2, CalculatePoints(GridD, state));
    }

    [Fact]
    public void TestHearts()
    {

        int[,] state =
        {
            { 0,6,6,6,6,0 },
            { 4,1,1,4,2,3 },
            { 0,6,6,6,6,0 },
        };

        Assert.Equal(5, CalculatePoints(GridE, state));
    }

    [Fact]
    public void TestRandom()
    {

        int[,] state =
        {
            { 0,0,0,0,0 },
            { 3,3,3,5,4 },
            { 1,5,2,4,4 },
            { 0,0,0,0,0 },
        };

        Assert.Equal(4, CalculatePoints(GridC, state));
    }

    [Fact]
    public void TestBigGrid()
    {
        int[,] state =
        {
            {1,1,1,0,0,1,3,1 },
            {1,1,3,3,3,1,1,1 },
            {0,6,1,1,1,1,5,0 },
            {0,1,1,0,0,3,5,0 },
            {0,1,1,1,6,5,5,0 },
            {1,1,1,1,2,2,5,1 },
            {2,2,1,0,0,1,2,1 },
        };

        Assert.Equal(15, CalculatePoints(Grids.GridB, state));
    }

    [Fact]
    public void TestWeirdGrid()
    {
        int[,] state =
        {
            {2,2 },
            {4,4 },
            {4,4 },
        };

        Assert.Equal(15, CalculatePoints(GridF, state));
    }
}
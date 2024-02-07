using Tester;

namespace Tests;

public class ScoreTests
{
    static readonly int[,] GridC =
        {
            { 0,0,0,0,0 },
            { 1,1,1,1,1 },
            { 1,1,1,1,1 },
            { 0,0,0,0,0 },
        };
    static readonly int[,] GridD =
        {
            { 2 }
        };
    static readonly int[,] GridE =
    {
        {0,1,1,1,1,0 },
        {3,1,1,3,1,1 },
        {0,1,1,1,1,0 },
    };

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

        Assert.Equal(10, Points.CalculatePoints(GridC, state));
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

        Assert.Equal(0, Points.CalculatePoints(GridC, state));
    }

    [Fact]
    public void TestDouble()
    {

        int[,] state =
        {
            { 1 }
        };

        Assert.Equal(2, Points.CalculatePoints(GridD, state));
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

        Assert.Equal(5, Points.CalculatePoints(GridE, state));
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

        Assert.Equal(4, Points.CalculatePoints(GridC, state));
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

        Assert.Equal(15, Points.CalculatePoints(Grids.GridB, state));
    }
}
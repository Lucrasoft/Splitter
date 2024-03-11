using Tester;

namespace Tests;

public class GameTests
{
    [Fact]
    public void SimpleGame()
    {
        var game = new Game(new Grid(ScoreTests.GridF));
        game.CurrentDice = (1, 1);
        game.Place(new Point(0, 0), 1);
        game.Place(new Point(0, 1), 1);
        game.Place(new Point(0, 2), 1);
        Assert.Equal(0, game.Rounds);
        Assert.Equal(5, game.GetPoints());
    }
    [Fact]
    public void TestFailingGame()
    {
        var game = new Game(new Grid(ScoreTests.GridF));
        game.CurrentDice = (1, 1);

        game.Place(new Point(0, 0), 1);
        try
        {
            game.Place(new Point(0, 0), 1);
            Assert.Fail();
        }
        catch (Exception)
        {
        }
        Assert.Equal(2, game.Rounds);
    }
    [Fact]
    public void TestBiggerGame()
    {
        var game = new Game(new Grid(ScoreTests.GridE));
        Assert.Equal(7, game.Rounds);
        game.CurrentDice = (1, 1);
        game.Place(new Point(1, 0), 1);
        game.CurrentDice = (2, 2);
        game.Place(new Point(2, 0), 2);
        game.CurrentDice = (6, 4);
        game.Place(new Point(0, 1), 6);
        game.Place(new Point(1, 1), 6);
        game.Place(new Point(2, 1), 4);
        Assert.Equal(2, game.Rounds);
        game.CurrentDice = (3, 1);
        game.Place(new Point(1, 2), 3);
        game.CurrentDice = (3, 3);
        game.Place(new Point(2, 2), 3);
        int[,] expected = {
            { 0,1,2,2,1,0 },
            { 6,6,4,6,4,4 },
            { 0,3,3,3,1,0 }
        };
        Assert.Equal(expected, game.State);
    }
}


using Tester;

public class Game
{
    public int[,] State { get; set; }
    public (int, int) CurrentDice { get; set; }
    public Grid Grid { get; set; }
    public int Rounds { get; set; }

    public Game(Grid grid)
    {
        this.Grid = grid;
        this.CurrentDice = RollDices();
        this.Rounds = CalculateRounds(grid);
        this.State = new int[grid.Height(), grid.Width()];
    }

    public void Place(Point point, int choice)
    {

        if (choice != CurrentDice.Item1 && choice != CurrentDice.Item2)
        {
            throw new Exception($"{choice} was not rolled please try again!");
        }

        if (Grid.Get(point) == Grids.EMPTY)
        {
            throw new Exception($"Can't place, tile blank / non-placeable place: {point.x},{point.y}");
        }


        if (State[point.y, point.x] != Grids.EMPTY)
        {
            throw new Exception($"Can't place on invalid tile it already has something place: {point.x},{point.y} value: {State[point.y, point.x]}");
        }

        Rounds -= 1;

        State[point.y, point.x] = choice;
        State[point.y, Grid.Width() - point.x - 1] = choice == CurrentDice.Item1 ? CurrentDice.Item2 : CurrentDice.Item1;
    }

    public int GetPoints()
    {
        return Points.CalculatePoints(Grid, State);
    }

    public static Random RND = new();
    public static (int, int) RollDices()
    {
        return (
            RND.Next(1, 7),
            RND.Next(1, 7)
        );
    }

    static int CalculateRounds(Grid grid)
    {
        int rounds = 0;

        foreach (var point in grid)
        {
            if (grid.Get(point) != Grids.EMPTY)
            {
                rounds++;
            }
        }

        rounds /= 2;

        return rounds;
    }
}
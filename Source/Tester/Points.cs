namespace Tester;

public class Points
{
    public static int CalculatePoints(int[,] grid, int[,] state)
    {
        //
        var points = 0;
        for (var i = 1; i <= 6; i++)
        {
            var stateCopy = (int[,])state.Clone();
            for (int k = 0; k < stateCopy.GetLength(0); k++)
            {
                for (int j = 0; j < stateCopy.GetLength(1); j++)
                {
                    if (stateCopy[k, j] != i) // If the value is not i, replace it with 0
                    {
                        stateCopy[k, j] = Grids.EMPTY;
                    }
                    else
                    {
                        stateCopy[k, j] = 1;
                    }
                }
            }

            var res = Matrix.CountIslands(stateCopy);
            points += res.Where(c => c.Length == i).Count() * i;

            List<Point> pointPlaces = [];
            List<Point> heartPlaces = [];

            for (int x = 0; x < grid.GetLength(0); x += 1)
            {
                for (int y = 0; y < grid.GetLength(1); y += 1)
                {
                    if (grid[x, y] == Grids.STAR)
                    {
                        pointPlaces.Add(new Point(
                            x, y
                            ));
                    }
                    if (grid[x, y] == Grids.HEART)
                    {
                        heartPlaces.Add(new Point(
                            x, y));
                    }


                }
            }

            foreach (var arr in res.Where(c => c.Length == i))
            {
                if (arr.Any(c =>
                   pointPlaces.Any(p => p.Equals(c))))
                {
                    points += i;
                    Logger.Log("BONUS POINTS");
                }
            }

            var heartsReached = 0;
            foreach (var star in heartPlaces)
            {
                if (state[star.x, star.y] == i)
                {
                    heartsReached += 1;
                }
            }
            // dont need to be giving bonus points if there are no hearts :)
            if (heartsReached != 0 && heartsReached == heartPlaces.Count)
            {
                points += 5;
            }
        }
        return points;
    }
}

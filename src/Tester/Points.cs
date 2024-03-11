namespace Tester;

public class Points
{
    public static int CalculatePoints(Grid grid, int[,] state)
    {
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
            var received = res.Where(c => c.Length == i).Count() * i;
            points += received;
            Logger.Log($"Groups of {i} points {received}");

            List<Point> starPlaces = [];
            List<Point> heartPlaces = [];

            foreach (var point in grid)
            {
                if (grid.Get(point) == Grids.STAR)
                {
                    starPlaces.Add(point);
                }
                else if (grid.Get(point) == Grids.HEART)
                {
                    heartPlaces.Add(point);
                }
            }

            foreach (var arr in res.Where(c => c.Length == i))
            {
                if (arr.Any(c =>
                   starPlaces.Any(p => p.Equals(c))))
                {
                    points += i;
                    Logger.Log($"Star points {i}");
                }
            }

            var heartsReached = 0;
            foreach (var star in heartPlaces)
            {
                if (state[star.y, star.x] == i)
                {
                    heartsReached += 1;
                }
            }
            // dont need to be giving bonus points if there are no hearts :)
            if (heartsReached != 0 && heartsReached == heartPlaces.Count)
            {
                points += 5;
                Logger.Log("Heart points 5");
            }
        }
        return points;
    }
}

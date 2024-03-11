using System.Collections;

namespace Tester
{
    public class Grid(int[,] grid) : IEnumerable<Point>
    {
        public int[,] _grid { get; set; } = grid;
        public void Set(Point point, int value)
        {
            _grid[point.y, point.x] = value;
        }
        public int Get(Point point)
        {
            return _grid[point.y, point.x];
        }
        public int Width()
        {
            return _grid.GetLength(1);
        }
        public int Height()
        {
            return _grid.GetLength(0);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    yield return new Point(j, i);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

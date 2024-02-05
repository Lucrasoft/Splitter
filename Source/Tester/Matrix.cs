using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tester
{
    public class Matrix
    {
        public static List<Tuple<int, int>> GetConnectedSets(int[][] grid)
        {
            List<Tuple<int, int>> connectedSets = new List<Tuple<int, int>>();
            int rows = grid.Length;
            int cols = grid[0].Length;
            bool[,] visited = new bool[rows, cols];

            // Define the directions (left, right, up, down)
            int[] dx = { 0, 0, -1, 1 };
            int[] dy = { -1, 1, 0, 0 };

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (!visited[i, j] && grid[i][j] != 0)
                    {
                        int count = 0;
                        int value = grid[i][j];

                        Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
                        queue.Enqueue(new Tuple<int, int>(i, j));
                        visited[i, j] = true;

                        while (queue.Count > 0)
                        {
                            Tuple<int, int> current = queue.Dequeue();
                            count++;

                            for (int k = 0; k < 4; k++)
                            {
                                int ni = current.Item1 + dx[k];
                                int nj = current.Item2 + dy[k];

                                if (ni >= 0 && ni < rows && nj >= 0 && nj < cols &&
                                    !visited[ni, nj] && grid[ni][nj] == value)
                                {
                                    queue.Enqueue(new Tuple<int, int>(ni, nj));
                                    visited[ni, nj] = true;
                                }
                            }
                        }

                        // Add the connected set to the result if it's less than or equal to the count
                        connectedSets.Add(new Tuple<int, int>(Math.Min(value, count), count));
                    }
                }
            }

            return connectedSets;
        }
    }

    
}

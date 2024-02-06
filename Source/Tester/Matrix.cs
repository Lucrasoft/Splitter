namespace Tester;

public class Matrix
{
    /// <summary>
    ///  Finds every group in the state
    /// </summary>
    /// <returns>Returns a list with sets of points of every group</returns>
    public static Point[][] CountIslands(int[,] state)
    {
        int n = state.GetLength(0);
        int m = state.GetLength(1);

        DisjointUnionSets dus = new DisjointUnionSets(n * m);

        for (int j = 0; j < n; j++)
        {
            for (int k = 0; k < m; k++)
            {
                if (state[j, k] == 0)
                    continue;

                // Check all 4 neighbours and do a union
                // with neighbour's set if neighbour is 
                // also 1
                if (j + 1 < n && state[j + 1, k] == 1)
                    dus.Union(j * m + k, (j + 1) * m + k);
                if (j - 1 >= 0 && state[j - 1, k] == 1)
                    dus.Union(j * m + k, (j - 1) * m + k);
                if (k + 1 < m && state[j, k + 1] == 1)
                    dus.Union(j * m + k, j * m + k + 1);
                if (k - 1 >= 0 && state[j, k - 1] == 1)
                    dus.Union(j * m + k, j * m + k - 1);
            }
        }

        Dictionary<int, List<Point>> islandSets = new Dictionary<int, List<Point>>();
        for (int j = 0; j < n; j++)
        {
            for (int k = 0; k < m; k++)
            {
                if (state[j, k] == 1)
                {
                    int root = dus.Find(j * m + k);
                    if (!islandSets.ContainsKey(root))
                        islandSets[root] = new List<Point>();
                    islandSets[root].Add(new Point(j, k));
                }
            }
        }

        // Convert islandSets to a jagged array of tuples
        Point[][] islandTuples = new Point[islandSets.Count][];
        int index = 0;
        foreach (var island in islandSets.Values)
        {
            islandTuples[index++] = island.ToArray();
        }

        return islandTuples;
    }
}


class DisjointUnionSets
{
    int[] rank, parent;
    int n;

    public DisjointUnionSets(int n)
    {
        rank = new int[n];
        parent = new int[n];
        this.n = n;
        MakeSet();
    }

    public void MakeSet()
    {
        // Initially, all elements are in their
        // own set.
        for (int i = 0; i < n; i++)
            parent[i] = i;
    }

    // Finds the representative of the set that x
    // is an element of
    public int Find(int x)
    {
        if (parent[x] != x)
        {
            // if x is not the parent of itself,
            // then x is not the representative of
            // its set.
            // so we recursively call Find on its parent
            // and move i's node directly under the
            // representative of this set
            parent[x] = Find(parent[x]);
        }

        return parent[x];
    }

    // Unites the set that includes x and the set
    // that includes y
    public void Union(int x, int y)
    {
        // Find the representatives (or the root nodes)
        // for x an y
        int xRoot = Find(x);
        int yRoot = Find(y);

        // Elements are in the same set, no need
        // to unite anything.
        if (xRoot == yRoot)
            return;

        // If x's rank is less than y's rank
        // Then move x under y so that depth of tree
        // remains less
        if (rank[xRoot] < rank[yRoot])
            parent[xRoot] = yRoot;

        // Else if y's rank is less than x's rank
        // Then move y under x so that depth of tree
        // remains less
        else if (rank[yRoot] < rank[xRoot])
            parent[yRoot] = xRoot;

        else // Else if their ranks are the same
        {
            // Then move y under x (doesn't matter
            // which one goes where)
            parent[yRoot] = xRoot;

            // And increment the result tree's
            // rank by 1
            rank[xRoot] = rank[xRoot] + 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Edge
{
    public int _Node1;
    public int _Node2;

    public int Weight;
}
public class PrimsMazeBuilder
{
    public int Columns = 5;
    public int Rows = 5;
    private int MaxWeight = 1;

    public List<Edge> EdgesExcluded;
    public List<Edge> EdgesIncluded;

    public List<int> NodesExcluded;
    public List<int> NodesIncluded;

    System.Random RandomGenerator = new System.Random();


    public void BuildMaze(int row, int col)
    {
        Rows = row;
        Columns = col;
        MaxWeight = row * col;
        RandomGenerator = new System.Random();
        NodesExcluded = new List<int>();
        NodesIncluded = new List<int>();

        EdgesExcluded = new List<Edge>();
        EdgesIncluded = new List<Edge>();
        Initialize();

        FindPaths();
    }

    #region Initialize
    private void Initialize()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                NodesExcluded.Add(CreateNode(row, col));
            }
        }

        for (int i = 0; i < NodesExcluded.Count; i++)
        {
            AddEdgesForNode(NodesExcluded[i]);
        }
    }
    #endregion

    #region FindPath
    private void FindPaths()
    {
        int node = NodesExcluded[0];
        NodesExcluded.Remove(node);
        NodesIncluded.Add(node);

        while (NodesExcluded.Count > 0)
        {
            FindNextLeastWeightEdge();
        }
    }
    private void FindNextLeastWeightEdge()
    {
        Edge edge = null;
        foreach (Edge e in EdgesExcluded)
        {
            if (edge != null && e.Weight > edge.Weight)
                continue;

            bool node1Included = NodesIncluded.Contains(e._Node1);
            bool node2Included = NodesIncluded.Contains(e._Node2);

            if (node1Included && node2Included)
                continue;

            if (node1Included || node2Included)
                edge = e;
        }

        EdgesIncluded.Add(edge);
        EdgesExcluded.Remove(edge);
        if (!NodesIncluded.Contains(edge._Node1))
        {
            NodesIncluded.Add(edge._Node1);
            NodesExcluded.Remove(edge._Node1);
        }
        if (!NodesIncluded.Contains(edge._Node2))
        {
            NodesIncluded.Add(edge._Node2);
            NodesExcluded.Remove(edge._Node2);
        }
    }
    #endregion

    #region Edge
    public void AddEdgesForNode(int node)
    {
        int north = GetNodeNorth(node);
        if (north >= 0)
            AddEdgeBetweenNodes(node, north);

        int south = GetNodeSouth(node);
        if (south >= 0)
            AddEdgeBetweenNodes(node, south);

        int west = GetNodeWest(node);
        if (west >= 0)
            AddEdgeBetweenNodes(node, west);

        int east = GetNodeEast(node);
        if (east >= 0)
            AddEdgeBetweenNodes(node, east);
    }
    public void AddEdgeBetweenNodes(int n1, int n2)
    {
        List<Edge> edges = EdgesExcluded.FindAll(e => ((e._Node1 == n1 && e._Node2 == n2) || (e._Node2 == n1 && e._Node1 == n2)));
        if (edges.Count == 0)
        {
            Edge edge = new Edge()
            {
                _Node1 = n1,
                _Node2 = n2
            };
            edge.Weight = RandomGenerator.Next(1, MaxWeight);
            EdgesExcluded.Add(edge);
        }
    }
    #endregion

    #region Nodes
    public int CreateNode(int row, int col)
    {
        return row * Columns + col;
    }

    public int GetNodeNorth(int n)
    {
        int row = n / Columns;
        int col = n - row * Columns;

        if (row == 0)
            return -1;

        return CreateNode(row - 1, col);
    }
    public int GetNodeSouth(int n)
    {
        int row = n / Columns;
        int col = n - row * Columns;

        if (row >= Rows - 1)
            return -1;

        return CreateNode(row + 1, col);
    }
    public int GetNodeWest(int n)
    {
        int row = n / Columns;
        int col = n - row * Columns;

        if (col == 0)
            return -1;

        return CreateNode(row, col - 1);
    }
    public int GetNodeEast(int n)
    {
        int row = n / Columns;
        int col = n - row * Columns;

        if (col >= Columns - 1)
            return -1;

        return CreateNode(row, col + 1);
    }
    #endregion

    #region FindWalls
    private bool HasPath(int n1, int n2)
    {
        List<Edge> edges = EdgesIncluded.FindAll(e => ((e._Node1 == n1 && e._Node2 == n2) || (e._Node2 == n1 && e._Node1 == n2)));
        return edges.Count > 0;
    }
    public bool NodeHasNorthWall(int row, int col)
    {
        int node = CreateNode(row, col);
        int north = GetNodeNorth(node);
        if (north < 0)
            return true;

        return HasPath(node, north) ? false : true;
    }
    public bool NodeHasSouthWall(int row, int col)
    {
        int node = CreateNode(row, col);
        int south = GetNodeSouth(node);
        if (south < 0)
            return true;

        return HasPath(node, south) ? false : true;
    }
    public bool NodeHasWestWall(int row, int col)
    {
        int node = CreateNode(row, col);
        int west = GetNodeWest(node);
        if (west < 0)
            return true;

        return HasPath(node, west) ? false : true;
    }
    public bool NodeHasEastWall(int row, int col)
    {
        int node = CreateNode(row, col);
        int east = GetNodeEast(node);
        if (east < 0)
            return true;

        return HasPath(node, east) ? false : true;
    }
    #endregion
}

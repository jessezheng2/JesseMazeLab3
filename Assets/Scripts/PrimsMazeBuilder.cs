using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ZhengJesse.Lab3
{
    #region Edge
    public class Edge
    {
        public int _Node1;
        public int _Node2;

        public int Weight;
    }
    #endregion

    #region PrimsMazeBuilder
    /*
     * This class finds all edges to be included in a maze by using
     * Prims' minimum spanning tree algorithm. This maze buidler ensures
     * that there is ONE AND ONLY ONE path between any 2 nodes of the maze.
     */
    public class PrimsMazeBuilder
    {
        //Number of columns of the maze. This is configurable on the Maze object. 
        public int Columns = 20;
        public int Rows = 20;

        // The maximum weight for the edges.
        private int MaxWeight = 1;

        //A list to keep track of the edges not have not been included in the maze
        public List<Edge> EdgesExcluded;
        /*
         * A list to keep track of the edges that are included in the maze.
         * When the algorithm is completed, the list will contains all
         * edges that are to be the path of the maze.
         */
        public List<Edge> EdgesIncluded;

        //A list of Nodes that are not processed.
        public List<int> NodesExcluded;
        //A list of Nodes that have been processed.
        public List<int> NodesIncluded;

        //A Random number generator
        System.Random RandomGenerator = new System.Random();

        /*
         * A method to create a row x col maze.
         * row: the number of rows for the maze
         * col: the number of columns for the maze
         */
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
        /*
         * This method initialize the maze with all nodes having 4 walls.
         * NodesExcluded is initialzed to contain all nodes (row*col in all)
         * Each node has an ID = (the row the node is in) *col + (the column the node is in)
         * Each Edge is between 2 nodes and an Edge object contains the ids of the 2 nodes.
         */
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
        /*
         * A method to find a minimum weight edge from the nodes processed to a node that
         * has not been processed. The edge found is then become a path of the maze.
         */
        private void FindPaths()
        {
            //Begin with the first node at row 0 and column 0 (i.e. ID=0)
            int node = NodesExcluded[0];
            NodesExcluded.Remove(node);
            NodesIncluded.Add(node);

            /*Loop until all nodes are processed*/

            while (NodesExcluded.Count > 0)
            {
                FindNextLeastWeightEdge();
            }
        }
        private void FindNextLeastWeightEdge()
        {
            /*Find the next least weighted edge to be included as the path of the maze.*/

            Edge edge = null;
            foreach (Edge e in EdgesExcluded)
            {
                if (edge != null && e.Weight > edge.Weight)
                    continue;

                bool node1Included = NodesIncluded.Contains(e._Node1);
                bool node2Included = NodesIncluded.Contains(e._Node2);

                //If the edge found connects 2 nodes that have been processed, don't use it.
                if (node1Included && node2Included)
                    continue;

                if (node1Included || node2Included)
                    edge = e;
            }
            /*Add the least weighted edge to the path collection*/
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
        /*
         * Add all edges of a specific node to the edge list.
         */
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
        /*
         * A maze is undirected graph. So make sure we only add 1 edge between any 2 nodes
         */
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

        /*
         * This methods calculate the id of the node located at row number = row, 
         * and column number = col.
         */
        public int CreateNode(int row, int col)
        {
            return row * Columns + col;
        }

        /*
         *  To calculate the id of the south adjacent node of the node having id=n.
         */
        public int GetNodeSouth(int n)
        {
            int row = n / Columns;
            int col = n - row * Columns;

            if (row == 0)
                return -1;

            return CreateNode(row - 1, col);
        }

        /*
         *  To calculate the id of the north adjacent node of the node having id=n.
         */
        public int GetNodeNorth(int n)
        {
            int row = n / Columns;
            int col = n - row * Columns;

            if (row >= Rows - 1)
                return -1;

            return CreateNode(row + 1, col);
        }
        /*
         *  To calculate the id of the west adjacent node of the node having id=n.
         */
        public int GetNodeWest(int n)
        {
            int row = n / Columns;
            int col = n - row * Columns;

            if (col == 0)
                return -1;

            return CreateNode(row, col - 1);
        }
        /*
         *  To calculate the id of the east adjacent node of the node having id=n.
         */
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
        /*
         * This method check to see if node n1 and n2 is connected.
         */
        private bool HasPath(int n1, int n2)
        {
            List<Edge> edges = EdgesIncluded.FindAll(e => ((e._Node1 == n1 && e._Node2 == n2) || (e._Node2 == n1 && e._Node1 == n2)));
            return edges.Count > 0;
        }
        /*
         * To determine if a node at row number = row and column number = col has a wall to the adjacent node in the north
         * If 2 nodes has a path, then there will be no wall, otherwise it will have a wall.
         */
        public bool NodeHasNorthWall(int row, int col)
        {
            int node = CreateNode(row, col);
            int north = GetNodeNorth(node);
            if (north < 0)
                return true;

            return HasPath(node, north) ? false : true;
        }
        /*
         * To determine if a node at row number = row and column number = col has a wall to the adjacent node in the south.
         * If 2 nodes has a path, then there will be no wall, otherwise it will have a wall.
         */
        public bool NodeHasSouthWall(int row, int col)
        {
            int node = CreateNode(row, col);
            int south = GetNodeSouth(node);
            if (south < 0)
                return true;

            return HasPath(node, south) ? false : true;
        }
        /*
         * To determine if a node at row number = row and column number = col has a wall to the adjacent node in the west.
         * If 2 nodes has a path, then there will be no wall, otherwise it will have a wall.
         */
        public bool NodeHasWestWall(int row, int col)
        {
            int node = CreateNode(row, col);
            int west = GetNodeWest(node);
            if (west < 0)
                return true;

            return HasPath(node, west) ? false : true;
        }
        /*
         * To determine if a node at row number = row and column number = col has a wall to the adjacent node in the east.
         * If 2 nodes has a path, then there will be no wall, otherwise it will have a wall.
         */
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
    #endregion
}

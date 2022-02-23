using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhengJesse.Lab3
{
    #region WallState
    [Flags]
    public enum WallState
    {
        //0000 - NO WALL
        //1111 - ALL WALLS
        WEST = 1, //0001
        EAST = 2, //0010
        NORTH = 4, //0100
        SOUTH = 8, //1000

        VISITED = 16, // 1000 0000
    }
    #endregion

    #region Position
    public struct Position
    {
        public int x;
        public int y;
    }
    #endregion

    #region AjacentCell
    public struct AjacentCell
    {
        public Position pos;
        public WallState sharedWall;
    }
    #endregion

    #region MazeBuilder
    public class MazeBuilder
    {
        private int Columns = 10;
        private int Rows = 10;
        private WallState[,] maze;

        /*
         * Determine the opopsite wall of a given wall
        */
        private static WallState GetOppositeWall(WallState wallState)
        {
            switch (wallState)
            {
                case WallState.WEST: return WallState.EAST;
                case WallState.EAST: return WallState.WEST;
                case WallState.NORTH: return WallState.SOUTH;
                case WallState.SOUTH: return WallState.NORTH;
                default: return WallState.NORTH;
            }
        }

        /*
         * Use Recursive Back Tracker Algorithm to remove the walls if the algorithm 
         * determines that a path needs to go throught the wall
         */
        private WallState[,] PerformRecursiveBacktracker()
        {
            System.Random rng = new System.Random();
            Stack<Position> stack = new Stack<Position>();
            //var position = new Position() { x = rng.Next(0, _Width), y = rng.Next(0, _Length) };
            var position = new Position { x = 0, y = 0 };
            maze[position.x, position.y] |= WallState.VISITED; //1000 0000
            stack.Push(position);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                var neighbours = GetUnvisitedAdjacentCells(current);
                if (neighbours.Count > 0)
                {
                    stack.Push(current);
                    var randIndex = rng.Next(0, neighbours.Count);
                    var randNeighbour = neighbours[randIndex];

                    var nPosition = randNeighbour.pos;
                    maze[current.x, current.y] &= ~randNeighbour.sharedWall;
                    maze[nPosition.x, nPosition.y] &= ~GetOppositeWall(randNeighbour.sharedWall);

                    maze[nPosition.x, nPosition.y] |= WallState.VISITED;
                    stack.Push(nPosition);
                }
            }
            return maze;
        }
        /*
         * Get all unvisited adjacent nodes of a node. 
        */
        private List<AjacentCell> GetUnvisitedAdjacentCells(Position p)
        {
            List<AjacentCell> list = new List<AjacentCell>();
            if (p.x > 0) // West Adjacent
                CheckAndAddAdjacentCell(p.x - 1, p.y, WallState.WEST, list);

            if (p.x < Columns - 1) // East Adjacent
                CheckAndAddAdjacentCell(p.x + 1, p.y, WallState.EAST, list);

            if (p.y > 0) //South
                CheckAndAddAdjacentCell(p.x, p.y - 1, WallState.SOUTH, list);

            if (p.y < Rows - 1) //North
                CheckAndAddAdjacentCell(p.x, p.y + 1, WallState.NORTH, list);

            return list;
        }
        /*
         * Add the adjacent node to the maze
         */
        private void CheckAndAddAdjacentCell(int x, int y, WallState sharedWall, List<AjacentCell> list)
        {
            if (maze[x, y].HasFlag(WallState.VISITED))
                return;

            Position p = new Position()
            {
                x = x,
                y = y
            };
            AjacentCell n = new AjacentCell()
            {
                pos = p,
                sharedWall = sharedWall
            };

            list.Add(n);
        }
        /*
         *  Initialize a rows x cols maze assuming every node has 4 walls.
         *  After that, use back track algorithm to break walls that a maze need
         *  to have paths throught them.
         */
        public WallState[,] Generate(int cols, int rows)
        {
            Columns = cols;
            Rows = rows;

            maze = new WallState[Columns, Rows];

            WallState allWall = WallState.WEST | WallState.NORTH | WallState.EAST | WallState.SOUTH;

            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                    maze[i, j] = allWall;
            }
            //return maze;
            return PerformRecursiveBacktracker();
        }
    }
    #endregion
}

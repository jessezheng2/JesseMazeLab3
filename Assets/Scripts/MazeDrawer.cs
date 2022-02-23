using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhengJesse.Lab3
{
    public class MazeDrawer : MonoBehaviour
    {
        [SerializeField]
        private Transform FloorPrefab = null;
        [SerializeField]
        private Transform WallPrefab = null;

        [SerializeField]
        [Range(1, 50)]
        private int Columns = 10;
        [SerializeField]
        private float Size = 1f;
        [SerializeField]
        [Range(1, 50)]
        private int Rows = 10;

        void Start()
        {
            //Intantiate a MazeBuilder and use back tracker algorithm to 
            //build a maze.
            var maze = new MazeBuilder().Generate(Columns, Rows);
            
            DrawFloor();
            DrawMaze(maze);
        }

        /*
         * Draw a floor to build the maze on
         */
        private void DrawFloor()
        {
            var floor = Instantiate(FloorPrefab, transform);
            floor.localScale = new Vector3(Columns, 1, Rows);
        }

        /*
         * Draw the maze by removing the walls in the maze paths.
         */
        private void DrawMaze(WallState[,] maze)
        {

            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    var cell = maze[i, j];
                    Vector3 center = new Vector3((-Columns / 2 + i) * Size, 0, (-Rows / 2 + j) * Size);

                    if (cell.HasFlag(WallState.NORTH))
                        DrawWall(center, 0, Size / 2, 0);

                    if (cell.HasFlag(WallState.WEST))
                        DrawWall(center, -Size / 2, 0, 90);

                    if (i == Columns - 1 && cell.HasFlag(WallState.EAST))
                        DrawWall(center, Size / 2, 0, 90);
                    if (j == 0 && cell.HasFlag(WallState.SOUTH))
                        DrawWall(center, 0, -Size / 2, 0);
                }
            }
        }
        /*
         * Draw a wall using the wall prefab.
         */
        private void DrawWall(Vector3 center, float xOffset, float zOffset, int yRotate)
        {
            var wall = Instantiate(WallPrefab, transform) as Transform;
            wall.position = center + new Vector3(xOffset, 0, zOffset);
            wall.localScale = new Vector3(Size, wall.localScale.y, wall.localScale.z);

            if (yRotate > 0)
                wall.eulerAngles = new Vector3(0, yRotate, 0);
        }
    }
}

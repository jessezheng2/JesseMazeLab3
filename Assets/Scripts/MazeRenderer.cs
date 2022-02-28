using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZhengJesse.Lab3
{
    #region MazeRenderer

    /*
     * This class renders the maze to the scene by using the 
     * pathes found by PrimsMazeBuilder.
     */
    public class MazeRenderer : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;

        [SerializeField]
        private Transform FloorPrefab = null;
        [SerializeField]
        private Transform WallPrefab = null;

        [SerializeField]
        [Range(1, 50)]
        private int Columns = 20;
        [SerializeField]
        private float Size = 3f;
        [SerializeField]
        [Range(1, 50)]
        private int Rows = 20;

        void Start()
        {
            //Create a PrimsMazeBuilder object and use it to find the least weighted path
            //for the maze so that there is one and only one path from any 2 nodes of the maze.
            PrimsMazeBuilder builder = new PrimsMazeBuilder();
            builder.BuildMaze(Rows, Columns);
            //DrawFloor();
            DrawMaze(builder);
        }
        /*
         * Render the maze by creating walls where there isn't a path in the edge collection
         * found by PrimsMazeBuilder. 
         */
        private void DrawMaze(PrimsMazeBuilder builder)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Vector3 center = new Vector3((-Columns / 2 + col) * Size, 0, (-Rows / 2 + row) * Size);

                    if (builder.NodeHasEastWall(row, col))
                        DrawWall(center, Size / 2, 0, 90);

                    if (builder.NodeHasNorthWall(row, col))
                    {
                        if (row != Rows - 1 || col != Columns - 1)
                            DrawWall(center, 0, Size / 2, 0);
                    }
                    if (builder.NodeHasWestWall(row, col))
                        DrawWall(center, -Size / 2, 0, 90);

                    if (builder.NodeHasSouthWall(row, col))
                    {
                        if ((row != 0) || (col != 0))
                            DrawWall(center, 0, -Size / 2, 0);
                    }

                    if(col==0 && row==0)
                    {
                        player.transform.position = center;
                    }
                }
            }
        }
        /*
         * Draw a wall between 2 nodes by using a wall prefab.
        */
        private void DrawWall(Vector3 center, float xOffset, float zOffset, int yRotate)
        {
            float platformHeight = 1;

            var wall = Instantiate(WallPrefab, transform) as Transform;
            wall.position = center + new Vector3(xOffset, platformHeight, zOffset);
            wall.localScale = new Vector3(Size, Size, wall.localScale.z);
           
            if (yRotate > 0)
                wall.eulerAngles = new Vector3(0, yRotate, 0);
        }
    }
    #endregion
}
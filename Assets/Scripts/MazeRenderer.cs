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
        private GameObject _Player;

        [SerializeField]
        private GameObject _ExitTrigger;

        [SerializeField]
        private Transform _WallPrefab = null;

        [SerializeField]
        private Transform _CoinPrefab = null;

        [SerializeField]
        private Transform _FloorPrefab = null;

        [SerializeField]
        [Range(1, 50)]
        private int _Columns = 20;
        [SerializeField]
        private float _Size = 3f;
        [SerializeField]
        [Range(1, 50)]
        private int _Rows = 20;

       
        public void BuildMaze()
        {
            //Create a PrimsMazeBuilder object and use it to find the least weighted path
            //for the maze so that there is one and only one path from any 2 nodes of the maze.
            PrimsMazeBuilder builder = new PrimsMazeBuilder();
            builder.BuildMaze(_Rows, _Columns);
            DrawFloor();
            DrawMaze(builder);
        }
        private void DrawFloor()
        {
            Vector3 center = new Vector3(-_Columns * _Size / 2, 0, -_Rows * _Size / 2);

            for (int row = 0; row < _Rows; row++)
            {
                for (int col = 0; col < _Columns; col++)
                {
                    var floor = Instantiate(_FloorPrefab, transform) as Transform;
                    floor.position = center + new Vector3(row*_Size, 0, col * _Size);
                    floor.localScale = new Vector3(_Size, 1, _Size);
                }
            }
            
        }
        /*
         * Render the maze by creating walls where there isn't a path in the edge collection
         * found by PrimsMazeBuilder. 
         */
        private void DrawMaze(PrimsMazeBuilder builder)
        {
            for (int row = 0; row < _Rows; row++)
            {
                for (int col = 0; col < _Columns; col++)
                {
                    Vector3 center = new Vector3((-_Columns / 2 + col) * _Size, 0, (-_Rows / 2 + row) * _Size);
                    int numberOfWalls = 0;

                    if (builder.NodeHasEastWall(row, col))
                    {
                        numberOfWalls++;
                        DrawWall(center, _Size / 2, 0, 90);
                    }
                    if (builder.NodeHasNorthWall(row, col))
                    {
                        if (row != _Rows - 1 || col != _Columns - 1)
                        {
                            numberOfWalls++;
                            DrawWall(center, 0, _Size / 2, 0);
                        }
                    }
                    if (builder.NodeHasWestWall(row, col))
                    {
                        numberOfWalls++;
                        DrawWall(center, -_Size / 2, 0, 90);
                    }
                    if (builder.NodeHasSouthWall(row, col))
                    {
                        if ((row != 0) || (col != 0))
                        {
                            numberOfWalls++;
                            DrawWall(center, 0, -_Size / 2, 0);
                        }
                    }
                    
                    if (numberOfWalls == 3)
                        AddCoin(center);
                    
                    if(col==_Columns-2 && row==_Rows-1)
                    //if (col == 0 && row == 0)
                    {
                        _Player.transform.position = center;
                    }
                    else if (col == _Columns - 1 && row == _Rows - 1)
                    {
                        center.z += 1;
                        _ExitTrigger.transform.position = center;
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

            var wall = Instantiate(_WallPrefab, transform) as Transform;
            wall.position = center + new Vector3(xOffset, platformHeight, zOffset);
            wall.localScale = new Vector3(_Size, _Size, wall.localScale.z);
           
            if (yRotate > 0)
                wall.eulerAngles = new Vector3(0, yRotate, 0);
        }

        private void AddCoin(Vector3 center)
        {
            var coin = Instantiate(_CoinPrefab, transform) as Transform;
            coin.position = center+new Vector3(0,2,0);

        }
    }
    #endregion
}
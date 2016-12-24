using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static int Width = 20;
    public static int Height = 20;
    public int Level = 1;

    public GameObject Wall;
    public GameObject Floor;
    public GameObject Door;
    public GameObject Plank;

    public Vector2 StartingPoint;

    void Start()
    {
        if (Width <= 0 || Height <= 0)
        {
            return;
        }

        var maze = new Maze(Width, Height, 0, 0);

        switch (Level)
        {
            case 1:
                RenderFloor();
                RenderMaze(maze);
                break;
            case 2:
                RenderLevel();
                break;
            default:
                break;
        }
    }

    public void RenderFloor()
    {
        var terrainGroup = new GameObject("FloorGroup").transform;

        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j < Height; j++)
            {
                var floor = Instantiate(Floor, new Vector3(i, -j), Quaternion.identity) as GameObject;
                floor.transform.SetParent(terrainGroup);
            }
        }
    }

    public void RenderLevel()
    {
        var terrainGroup = new GameObject("TerrainGroup").transform;

        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j < Height; j++)
            {
                var floor = Instantiate(Floor, new Vector3(i, j), Quaternion.identity) as GameObject;
                floor.transform.SetParent(terrainGroup);

                if (i == 0 || j == 0 || i == Width - 1 || j == Height -1)
                {
                    var wall = Instantiate(Wall, new Vector3(i, j), Quaternion.identity) as GameObject;
                    wall.transform.SetParent(terrainGroup);
                }
            }
        }
    }

    public void RenderMaze(Maze maze)
    {
        Func<int, Quaternion> q = x => {
            var rotVect = transform.rotation.eulerAngles;
            rotVect.z = x;
            return Quaternion.Euler(rotVect);
        };

        var group = new GameObject("MazeGroup").transform;

        // set planks
        for (var i = 0; i < maze.Width; i++)
        {
            for (var j = 0; j < maze.Height; j++)
            {
                var cell = maze.Cells[i, j];
                var vect = new Vector3(j, -i);

                if (cell.UpWall)
                {
                    var obj = Instantiate(Plank, vect, q(270)) as GameObject;
                    obj.transform.SetParent(group);
                }

                if (cell.DownWall)
                {
                    var obj = Instantiate(Plank, vect, q(90)) as GameObject;
                    obj.transform.SetParent(group);
                }

                if (cell.LeftWall)
                {
                    var obj = Instantiate(Plank, vect, q(0)) as GameObject;
                    obj.transform.SetParent(group);
                }

                if (cell.RightWall)
                {
                    var obj = Instantiate(Plank, vect, q(180)) as GameObject;
                    obj.transform.SetParent(group);
                }
            }
        }

        // set exit door
        var exitCell = Utils.PickItem2(maze.Cells);
        Instantiate(Door, new Vector3(exitCell.Y, -exitCell.X), Quaternion.identity);
    }
}

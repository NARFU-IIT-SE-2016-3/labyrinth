using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int Width = 20;
    public static int Height = 20;

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
        //RenderMaze(maze);
        RenderLevel();
    }

    public void RenderLevel()
    {
        var terrainGroup = new GameObject("TerrainGroup").transform;

        var doorPos = new Vector3(Utils.RandInt(1, Width - 1), 0);

        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j < Height; j++)
            {
                var floor = Instantiate(Floor, new Vector3(i, j), Quaternion.identity) as GameObject;
                floor.transform.SetParent(terrainGroup);

                if (i == doorPos.x && j == doorPos.y)
                {
                    Instantiate(Door, doorPos, Quaternion.identity);
                }
                else if (i == 0 || j == 0 || i == Width - 1 || j == Height -1)
                {
                    var wall = Instantiate(Wall, new Vector3(i, j), Quaternion.identity) as GameObject;
                    wall.transform.SetParent(terrainGroup);
                }
            }
        }
    }

    public void RenderMaze(Maze maze)
    {
        for (var i = 0; i < maze.Width; i++)
        {
            for (var j = 0; j < maze.Height; j++)
            {
                var cell = maze.Cells[i, j];
                var x = i;
                var y = -j;

                if (cell.UpWall)
                {
                    var rotation = 270;
                    var rotVect = transform.rotation.eulerAngles;
                    rotVect.z = rotation;
                    var quat = Quaternion.Euler(rotVect);
                    Instantiate(Plank, new Vector3(x, y), quat);
                }

                if (cell.DownWall)
                {
                    var rotation = 90;
                    var rotVect = transform.rotation.eulerAngles;
                    rotVect.z = rotation;
                    var quat = Quaternion.Euler(rotVect);
                    Instantiate(Plank, new Vector3(x, y), quat);

                }

                if (cell.LeftWall)
                {
                    var rotation = 0;
                    var rotVect = transform.rotation.eulerAngles;
                    rotVect.z = rotation;
                    var quat = Quaternion.Euler(rotVect);
                    Instantiate(Plank, new Vector3(x, y), quat);
                }

                if (cell.RightWall)
                {
                    var rotation = 180;
                    var rotVect = transform.rotation.eulerAngles;
                    rotVect.z = rotation;
                    var quat = Quaternion.Euler(rotVect);
                    Instantiate(Plank, new Vector3(x, y), quat);
                }
            }
        }
    }
}

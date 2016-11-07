using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static int Width = 20;
    public static int Height = 20;

    public GameObject Wall;
    public GameObject Floor;
    public GameObject Door;

    public Vector2 StartingPoint;

    // Use this for initialization
    void Start()
    {
        if (Width <= 0 || Height <= 0)
        {
            return;
        }

        //var maze = Maze.GenerateOrthogonal(Width, Height, (int)StartingPoint.x, (int)StartingPoint.y, false);
        //RenderMaze(maze);
        RenderLevel();
    }
	
	// Update is called once per frame
	void Update()
    {
	
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

    public void RenderMaze(Maze.Cell[,] cells)
    {
        for (var i = 0; i < cells.GetLength(0); i++)
        {
            for (var j = 0; j < cells.GetLength(1); j++)
            {
                Debug.Log(string.Format("{0}, {1}: {2}", i, j, cells[i, j].IsEdge));

                if (cells[i, j].IsEdge)
                {
                    Instantiate(Wall, new Vector3(i / 2, j / 2), Quaternion.identity);
                }
            }
        }
    }
}

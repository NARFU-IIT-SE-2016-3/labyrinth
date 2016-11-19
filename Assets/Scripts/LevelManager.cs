using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int Width = 20;
    public static int Height = 20;

    public GameObject Wall;
    public GameObject Floor;
    public GameObject Door;

    public Vector2 StartingPoint;

    void Start()
    {
        if (Width <= 0 || Height <= 0)
        {
            return;
        }

        var maze = new Maze(Width, Height, (int)StartingPoint.x, (int)StartingPoint.y);
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

    public void RenderMaze(Maze.Cell[,] cells)
    {
        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j < Height; j++)
            {
                var go = GetCellObject(cells[i, j]);
                Instantiate(go, new Vector3(i, j), Quaternion.identity);
            }
        }
    }

    public GameObject GetCellObject(Maze.Cell cell)
    {
        var bin = cell.GetBinaryWalls();

        //switch (bin)
        //{
        //    case 0x0000: return '\u253c';
        //    case 0x0001: return '\u2524';
        //    case 0x0010: return '\u251c';
        //    case 0x0011: return '\u2502';
        //    case 0x0100: return '\u2534';
        //    case 0x0101: return '\u2518';
        //    case 0x0110: return '\u2514';
        //    //case 0x0111: return '\u2575';
        //    case 0x1000: return '\u252c';
        //    case 0x1001: return '\u2510';
        //    case 0x1010: return '\u250c';
        //    //case 0x1011: return '\u2577';
        //    case 0x1100: return '\u2500';
        //    //case 0x1101: return '\u2573';
        //    //case 0x1110: return '\u2576';
        //    //case 0x1111: return '\u2588';
        //    default: return ' '; //'\u2588';
        //}

        return new GameObject();
    }
}

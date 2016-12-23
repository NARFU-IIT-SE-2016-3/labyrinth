using System;
using System.Collections.Generic;

/// <summary>
/// Recursive Backtracker
/// (Depth-First Search)
/// </summary>
public class Maze
{
    public Cell[,] Cells { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    private Cell currentCell;
    private Stack<Cell> stack;
    private Random rng;

    public class Cell
    {
        public bool IsVisited = false;
        public bool UpWall = true;
        public bool DownWall = true;
        public bool LeftWall = true;
        public bool RightWall = true;

        public int X { get; private set; }
        public int Y { get; private set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int GetBinaryWalls()
        {
            var init = 0x0000;

            if (UpWall)
            {
                init += 0x1000;
            }

            if (DownWall)
            {
                init += 0x0100;
            }

            if (LeftWall)
            {
                init += 0x0010;
            }

            if (RightWall)
            {
                init += 0x0001;
            }

            return init;
        }
    }

    public Maze(int width, int height, int startX, int startY)
    {
        Width = width;
        Height = height;
        Cells = new Cell[width, height];
        stack = new Stack<Cell>();
        rng = new Random();

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                Cells[i, j] = new Cell(i, j);
            }
        }

        currentCell = Cells[startX, startY];
        currentCell.IsVisited = true;
        currentCell.UpWall = false; // entrance

        while (!IsEveryCellVisited())
        {
            var neighbors = GetUnvisitedNeighbors(currentCell);

            if (neighbors.Count > 0)
            {
                var randCell = GetRandomElement(neighbors);

                stack.Push(currentCell);

                RemoveWall(currentCell, randCell);

                currentCell = randCell;
                currentCell.IsVisited = true;
            }
            else if (stack.Count > 0)
            {
                currentCell = stack.Pop();
            }
        }
    }

    private bool IsEveryCellVisited()
    {
        var isPreviousVisited = true;

        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j < Height; j++)
            {
                isPreviousVisited = isPreviousVisited && Cells[i, j].IsVisited;
            }
        }

        return isPreviousVisited;
    }

    private List<Cell> GetUnvisitedNeighbors(Cell cell)
    {
        var neighbors = new List<Cell>();

        if (cell.X != 0 && !Cells[cell.X - 1, cell.Y].IsVisited)
        {
            neighbors.Add(Cells[cell.X - 1, cell.Y]);
        }

        if (cell.X != Width - 1 && !Cells[cell.X + 1, cell.Y].IsVisited)
        {
            neighbors.Add(Cells[cell.X + 1, cell.Y]);
        }

        if (cell.Y != 0 && !Cells[cell.X, cell.Y - 1].IsVisited)
        {
            neighbors.Add(Cells[cell.X, cell.Y - 1]);
        }

        if (cell.Y != Height - 1 && !Cells[cell.X, cell.Y + 1].IsVisited)
        {
            neighbors.Add(Cells[cell.X, cell.Y + 1]);
        }

        return neighbors;
    }

    private T GetRandomElement<T>(List<T> list)
    {
        var randIdx = rng.Next(0, list.Count);
        return list[randIdx];
    }

    private void RemoveWall(Cell cell1, Cell cell2)
    {
        if (cell1.X < cell2.X)
        {
            cell1.DownWall = false;
            cell2.UpWall = false;
        }
        else if (cell1.X > cell2.X)
        {
            cell1.UpWall = false;
            cell2.DownWall = false;
        }
        else if (cell1.Y < cell2.Y)
        {
            cell1.RightWall = false;
            cell2.LeftWall = false;
        }
        else if (cell1.Y > cell2.Y)
        {
            cell1.LeftWall = false;
            cell2.RightWall = false;
        }
    }
}
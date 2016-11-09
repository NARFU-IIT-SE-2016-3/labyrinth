using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Recursive Backtracker
/// (Depth-First Search)
/// </summary>
public class Maze
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Vector2 StartingPoint { get; private set; }

    public class Cell
    {
        private Wall walls;

        private enum Wall
        {
            Initial = 0x1111,
            Up = 0x1000,
            Down = 0x0100,
            Left = 0x0010,
            Right = 0x0001
        }

        public Cell()
        {
            walls = Wall.Initial;
        }

        public void ClearWall()
        {

        }
    }

    public Maze(int width, int height, Vector2 startingPoint)
    {
        Width = width;
        Height = height;
        StartingPoint = startingPoint;
        Generate();
    }

    private void Generate()
    {

    }
}

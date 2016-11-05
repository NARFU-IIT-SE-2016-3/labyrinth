using System;
using System.Collections.Generic;

public class Maze
{
    public static bool Randomize { get; set; }
    private static Random rand = new Random(); // You probably want to seed this

    public class Cell
    {
        public List<Cell> Links = new List<Cell>();
        public bool ExitCell = false;

        public bool IsLinked(Cell c) // Not actually used during generation logic, but this can be useful for drawing
        {
            return Links.Contains(c) || c.Links.Contains(this);
        }

        public bool IsEdge
        {
            get { return Links.Count == 0; }
        }
    }

    private class GenerationCell : Cell // Helper class to store information not relevant to the end result
    {
        public bool Visited = false;
        public GenerationCell Previous = null; // For stackless backtracking

        public GenerationCell() { }
    }

    public static void Seed(int seed)
    {
        rand = new Random(seed);
    }

    // The actual maze generation is adaptible to any topology
    private static void MakeMaze(GenerationCell start)
    {
        GenerationCell current = start, exit = start, next;

        // To find the farthest exit, we keep track of “tree” depth
        // Potential improvement: Store this information in the cell itself, for fancy stuff later on
        ulong depth = 0;
        ulong max = 0; 

        while (true) // This ain't Lisp, I ain't recursin'
        {
            current.Visited = true;
            bool natural = true;

            if (Randomize)
            {
                Shuffle<Cell>(current.Links); // This is a destructive function. May want to rewrite to use a temporary clone list for iteration
            }

            for (int i = 0; i < current.Links.Count; i++)
            {
                next = (GenerationCell)current.Links[i]; // We cast for access to .Previous, which regular cells do not have // Note: This incurs no extra penalty so using it in the main body of a loop is A-OK

                if (next.Previous == current) // We obviously don't want to mess with a link if we've already gone that way
                    continue;

                if (next.Visited) // It was already visited via some other route, so this is not what we're looking for
                {
                    current.Links.RemoveAt(i--);
                    continue;
                }

                // It's an unexplored link
                if (++depth > max) // Since we're exploring, and want to keep track of the highest later on
                {
                    exit = next;
                    max = depth;
                }

                next.Previous = current;
                current = next;
                natural = false; // Ugly helper variable since we can't continue out of multiple loops that easily
                break;
            }

            if (!natural) // The loop didn't terminate naturally, so we found a new node and are considering from there
                continue;

            // We explored all links and found none - dead end cell
            if (current.Previous == null) // The starting cell has no previous cell
            {
                // The function can safely end here, since if we've backtraced all the way to the
                // starting cell and found no more link, the entire maze has been explored.
                exit.ExitCell = true;
                return;
            }

            current = current.Previous;
            depth--;
        }   // The endless loop repeats here
    }

    // Generates an array of cells, and a maze to go with them
    public static Cell[,] GenerateOrthogonal(int Width, int Height, int XEntrance, int YEntrance, bool Cyclic)
    {
        if (XEntrance >= Width || YEntrance >= Height)
        {
            throw new ArgumentOutOfRangeException(string.Format("Starting coordinates out of range. {0},{1},{2},{3}", Width, Height, XEntrance, YEntrance));
        }

        GenerationCell[,] maze = new GenerationCell[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                maze[x, y] = new GenerationCell(); // initialize with non-connected cells
            }
        }

        // Generate links to adjacent cells, since this is an orthogonal maze
        // To-do: Maybe encapsulate the bounds-checking and link generation process inside a small function, and call it on a list of locations instead?
        //        Not going to bother right now since it won't significantly reduce SLOC count, but for non-2D mazes this would be nice
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (x > 0)
                    maze[x, y].Links.Add(maze[x - 1, y]);
                else if (Cyclic)
                    maze[x, y].Links.Add(maze[Width - 1, y]);

                if (y > 0)
                    maze[x, y].Links.Add(maze[x, y - 1]);
                else if (Cyclic)
                    maze[x, y].Links.Add(maze[x, Height - 1]);

                if (x + 1 < Width)
                    maze[x, y].Links.Add(maze[x + 1, y]);
                else if (Cyclic)
                    maze[x, y].Links.Add(maze[0, y]);

                if (y + 1 < Height)
                    maze[x, y].Links.Add(maze[x, y + 1]);
                else if (Cyclic)
                    maze[x, y].Links.Add(maze[x, 0]);
            }
        }

        MakeMaze(maze[XEntrance, YEntrance]);

        return maze;
    }

    private static void Shuffle<T>(List<T> list) // pseudo-polymorphism hack, cheaper than boxing/unboxing. Haskell has spoiled me.
    {
        for (int n = list.Count - 1; n > 0; n--) // Note: Fisher-Yates shuffle
        {
            int k = rand.Next(n + 1);
            T tmp = list[k];
            list[k] = list[n];
            list[n] = tmp;
        }
    }
}

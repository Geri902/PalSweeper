using System.Collections.Generic;
using System.Linq;
using Godot;
public class BasicGameMaster
{
    RandomNumberGenerator rnd = new RandomNumberGenerator();
    Vector2I[] directions =
    {
        //these comments are not AI generated, I just know I will get confused
        new Vector2I(-1,0),  // Left
        new Vector2I(-1,-1), // Top Left
        new Vector2I(0,-1),  // Top
        new Vector2I(1,-1),  // Top Right
        new Vector2I(1,0),   // Right
        new Vector2I(1,1),   // Bottom Right
        new Vector2I(0,1),   // Bottom
        new Vector2I(-1,1)   // Bottom Left
    };
    Ground[,] map; 
    Vector2I[] mineLocations;
    int width;
    int height;
    int mines;

    public BasicGameMaster(int width = 3, int height = 3, int mines = 3, int startX = 0, int startY = 0)
    {
        this.width = width;
        this.height = height;
        this.mines = mines;

        initGame(startX, startY);
    }

    public void RegenerateMap(int width, int height, int mines, int startX = 0, int startY = 0)
    {
        this.width = width;
        this.height = height;
        this.mines = mines;

        map = null;
        mineLocations = null;

        initGame(startX, startY);
    }

    private void initGame(int startX, int startY)
    {
        rnd.Randomize();

        map = new Ground[width, height];
        mineLocations = new Vector2I[mines];

        GenerateMines(startX, startY);
        GenerateMap();

    }

    private void GenerateMines(int startX, int startY)
    {
        if (mines < (width * height)) // check if generatable
        {
            List<(int x,int y)> allPositions = new List<(int x, int y)>();

            for (int y = 0; y < height; y++) // Generate all possible locations
            {
                for (int x = 0; x < width; x++)
                {
                    allPositions.Add((x, y));
                }
            }

            allPositions.Remove((startX, startY));
            
            for (int i = 0; i < mines; i++) // this way we don't need to check for overlapping mines, because they can't exist
            {
                int position = rnd.RandiRange(0, (height * width) - (i + 1));
                (int x, int y) = allPositions[position];
                mineLocations[i] = new Vector2I(x, y);
                allPositions.RemoveAt(position);
            }

        }
    }

    private void GenerateMap()
    {
        for (int y = 0; y < height; y++) 
        {
            for (int x = 0; x < width; x++)
            {
                int mineCount = CheckForMines(x, y);
                bool mine = mineLocations.Any(z=>Vector2ICompare(z, x, y));
                map[x, y] = new Ground(mine, mineCount);
            }
        }
    }

    private int CheckForMines(int x, int y)
    {
        int count = 0;

        foreach (Vector2I direction in directions)
        {
            int newX = x + direction.X;
            int newY = y + direction.Y;

            if (IndexInBounds(newX, newY))
            {
                if (mineLocations.Any(x=>Vector2ICompare(x, newX, newY)))
                {
                    count ++;
                }
            }
        }

        return count;
    }

    private bool IndexInBounds(int x, int y)
    {
        if (x < width && x > -1)
        {
            if (y < height && y > -1)
            {
                return true;
            }
        }
        return false;
    }

    private bool Vector2ICompare(Vector2I vector, int x, int y)
    {
        if (vector.X == x && vector.Y == y)
        {
            return true;
        }
        return false;
    }


    //these codes are for debugging
    public void PrintMap()
    {
        string output = $"---\nW:{width} H:{height} M:{mines}\n";

        for (int y = 0; y < height; y++) 
        {
            for (int x = 0; x < width; x++)
            {
                Ground ground = map[x, y];
                if (ground.GetMine())
                {
                    output += "X";
                }
                else
                {
                    output += $"{ground.GetMinesAround()}";
                }
            }
            if (y != height - 1)
            {
                output += "\n";
            }
        }

        GD.Print(output);
    }
    
}
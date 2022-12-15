namespace AdventOfCode2022;
public static class Day08
{
    private record struct Direction(int Row, int Col);

    private static Direction[] directions = new[] { new Direction(-1, 0), new Direction(1, 0), new Direction(0, 1), new Direction(0, -1) };

    private record struct Tree(int Height, bool Visible, int ScenicScore);

    private static Tree[][] GetTrees(string path) => File.ReadAllLines(path)
            .Select(r => r.Select(c => new Tree(c - '0', false, 0)).ToArray())
            .ToArray();

    public static int One(string path)
    {
        var trees = GetTrees(path);

        for (int i = 0; i < trees.Length; i++)
            for (int j = 0; j < trees[0].Length; j++) 
                trees[i][j].Visible = OnEdge(trees, i, j) || Visible(trees, i, j);
            
        return trees.SelectMany(t => t).Where(t => t.Visible).Count();
    }

    public static int Two(string path)
    {
        var trees = GetTrees(path);

        for (int i = 0; i < trees.Length; i++)
            for (int j = 0; j < trees[0].Length; j++)
                trees[i][j].ScenicScore = CalculateScenicScore(trees, i, j);

        return trees.SelectMany(t => t).Max(t => t.ScenicScore);
    }

    private static bool Visible(Tree[][] trees, int row, int col)
    {
        bool visible = false;

        foreach (var d in directions)
        {
            int nextRow = row + d.Row;
            int nextCol = col + d.Col;
            visible = true;

            while (InBounds(trees, nextRow, nextCol) && visible)
            {
                visible = trees[row][col].Height > trees[nextRow][nextCol].Height;
                nextRow = nextRow + d.Row;
                nextCol = nextCol + d.Col;
            }

            if (visible)
                break;
        }
        return visible;
    }

    private static int CalculateScenicScore(Tree[][] trees, int row, int col)
    {
        int score = 1;
        foreach (var d in directions)
        {
            int nextRow = row + d.Row;
            int nextCol = col + d.Col;
            int distance = 0;

            while (InBounds(trees, nextRow, nextCol))
            {
                distance++;

                if (!(trees[row][col].Height > trees[nextRow][nextCol].Height))
                    break;

                nextRow = nextRow + d.Row;
                nextCol = nextCol + d.Col;
            }

            score *= distance;
        }
        return score;
    }

    private static bool InBounds(Tree[][] trees, int row, int col) => row >= 0 && row < trees.Length && col >= 0 && col < trees[0].Length;
    private static bool OnEdge(Tree[][] trees, int row, int col) => row == 0 || col == 0 || row == trees.Length - 1 || col == trees[0].Length - 1;
}

namespace AdventOfCode2022;
public static class Day03
{
    public static int One(string path) => File.ReadAllLines(path)
        .Select(l => l[0..(l.Length / 2)].Intersect(l[(l.Length / 2)..]).Single())
        .Select(c => GetPriority(c))
        .Sum();
    
    public static int Two(string path) => File.ReadAllLines(path)
            .Select((l, i) => (Line: l, GroupNumber: i / 3))
            .GroupBy(x => x.GroupNumber)
            .Select(g => g.Select(x => x.Line.AsEnumerable())
                .Aggregate((current, next) => current.Intersect(next))
                .Single())
            .Select(c => GetPriority(c))
            .Sum();

    private static int GetPriority(char item) => char.IsUpper(item) ? item - 38 : item - 96;
}

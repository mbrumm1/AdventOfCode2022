namespace AdventOfCode2022;
public static class Day04
{
    public static int One(string path) => GetSectionIds(path)
        .Select(s =>
            (s.X1 >= s.X2 && s.Y1 <= s.Y2) ||
            (s.X2 >= s.X1 && s.Y2 <= s.Y1))
        .Count(isFullyContained => isFullyContained);

    public static int Two(string path) => GetSectionIds(path)
        .Select(s => 
            s.X1 <= s.Y2 && s.Y1 >= s.X2)
        .Count(isOverlapping => isOverlapping);

    private static IEnumerable<(int X1, int Y1, int X2, int Y2)> GetSectionIds(string path) => File.ReadAllLines(path)
        .Select(l => (
            X1: GetSectionId(l, 0, 0),
            Y1: GetSectionId(l, 0, 1),
            X2: GetSectionId(l, 1, 0),
            Y2: GetSectionId(l, 1, 1)));

    private static int GetSectionId(string pair, int elf, int section) => int.Parse(pair.Split(',')[elf].Split('-')[section]);
}

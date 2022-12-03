namespace AdventOfCode2022;
public static class Day01
{
    public static int One(string path) => GetCalories(path).Max();
    public static int Two(string path) => GetCalories(path).OrderByDescending(x => x).Take(3).Sum();
    public static IEnumerable<int> GetCalories(string path) => File.ReadAllText(path)
        .Split("\n\n")
        .Select(s => s.Split("\n").Select(n => int.Parse(n)).Sum());
}

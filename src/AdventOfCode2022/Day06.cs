namespace AdventOfCode2022;
public static class Day06
{
    public static int One(string path) => File.ReadAllText(path).Map(s => GetCharactersProcessed(s, 4));
    public static int Two(string path) => File.ReadAllText(path).Map(s => GetCharactersProcessed(s, 14));
    private static int GetCharactersProcessed(string buffer, int markerLength) => Enumerable.Range(0, buffer.Length - markerLength)
        .Select(x => (Start: x, Marker: buffer[x..(x + markerLength)]))
        .Where(x => x.Marker.Distinct().Count() == markerLength)
        .First().Start + markerLength;
}

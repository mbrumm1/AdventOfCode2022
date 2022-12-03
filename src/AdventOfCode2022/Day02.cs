namespace AdventOfCode2022;
public static class Day02
{
    private const int rock = 1;
    private const int paper = 2;
    private const int scissors = 3;
    private const int loss = 0;
    private const int draw = 3;
    private const int win = 6;
    // Second column represents what you should play in response. Rock (X), Paper (Y), Scissors (Z)
    public static int One(string path) => RunStrategy(path, strategyOne);
    // Second column represents what the outcome should be. Loss (X), Draw (Y), Win (Z).
    public static int Two(string path) => RunStrategy(path, strategyTwo);
    private static int RunStrategy(string path, Func<string[], int> strategy) => File.ReadAllLines(path)
        .Select(l => l.Split(" "))
        .Select(strategy)
        .Sum();
    private static Func<string[], int> strategyOne => input => (input[0], input[1]) switch
    {
        ("A", "X") => rock + draw,
        ("A", "Y") => paper + win,
        ("A", "Z") => scissors + loss,
        ("B", "X") => rock + loss,
        ("B", "Y") => paper + draw,
        ("B", "Z") => scissors + win,
        ("C", "X") => rock + win,
        ("C", "Y") => paper + loss,
        ("C", "Z") => scissors + draw,
        (_, _) => throw new ArgumentException()
    };
    private static Func<string[], int> strategyTwo => input => (input[0], input[1]) switch
    {
        ("A", "X") => scissors + loss,
        ("A", "Y") => rock + draw,
        ("A", "Z") => paper + win,
        ("B", "X") => rock + loss,
        ("B", "Y") => paper + draw,
        ("B", "Z") => scissors + win,
        ("C", "X") => paper + loss,
        ("C", "Y") => scissors + draw,
        ("C", "Z") => rock + win,
        (_, _) => throw new ArgumentException()
    };
}
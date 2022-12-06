namespace AdventOfCode2022;
public static class Extensions
{
    public static void WriteLine<T>(this T @this) => Console.WriteLine(@this);

    public static TResult Map<TSource, TResult>(this TSource @this, Func<TSource, TResult> map) => map(@this);
}

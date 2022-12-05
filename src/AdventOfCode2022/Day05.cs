using System.Security.Principal;

namespace AdventOfCode2022;
public static class Day05
{
    private static string[] GetLines(string path) => File.ReadAllLines(path);

    private static int GetSeparatorIndex(string[] lines)
    {
        int i = 0;
        while (!string.IsNullOrWhiteSpace(lines[i])) i++;
        return i;
    }

    private static int GetStackCount(string[] lines, int separatorIndex) => lines[separatorIndex - 1][^2] - '0';

    private static Stack<char>[] CreateStacks(int count)
    {
        Stack<char>[] stacks = new Stack<char>[count];
        for (int j = 0; j < stacks.Length; j++)
            stacks[j] = new Stack<char>();
        return stacks;
    }

    private static Stack<char>[] InitializeCrates(string[] lines, Stack<char>[] stacks, int separatorIndex)
    {
        for (int i = separatorIndex - 2; i > -1; i--)
            InitializeCrateLayer(stacks, lines[i]);
        return stacks;
    }

    private static Stack<char>[] InitializeCrateLayer(Stack<char>[] stacks, string crateLayer)
    {
        int stackIndex = 0;
        for (int i = 1; i < crateLayer.Length - 1; i += 4)
        {
            char crate = crateLayer[i];
            if (!char.IsWhiteSpace(crate))
                stacks[stackIndex].Push(crateLayer[i]);
            stackIndex++;
        }
        return stacks;
    }

    private static Stack<char>[] RunProcedure(Stack<char>[] stacks, string[] steps)
    {
        for (int i = 0; i < steps.Length; i++)
        {
            int[] values = GetStepValues(steps[i]);
            RunStep(stacks, values[0], values[1], values[2]);
        }
        return stacks;
    }

    private static int[] GetStepValues(string step) => step
        .Split(new[] { "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries)
        .Select(x => int.Parse(x))
        .ToArray();

    private static Stack<char>[] RunStep(Stack<char>[] stacks, int quantity, int from, int to)
    {
        for (int i = 0; i < quantity; i++) 
        {
            char removed = stacks[from - 1].Pop();
            stacks[to - 1].Push(removed);
        }
        return stacks;
    }

    private static string GetMessage(Stack<char>[] stacks, int stackCount) 
    {
        List<char> crates = new();
        for (int i = 0; i < stacks.Length; i++)
        {
            if (stacks[i].Any())
                crates.Add(stacks[i].Peek());
        }
        return string.Concat(crates);
    }

    public static string One(string path)
    {
        string[] lines = GetLines(path);
        int separatorIndex = GetSeparatorIndex(lines);
        int stackCount = GetStackCount(lines, separatorIndex);
        Stack<char>[] stacks = CreateStacks(stackCount);
        stacks = InitializeCrates(lines, stacks, separatorIndex);
        string[] steps = lines[(separatorIndex + 1)..];
        stacks = RunProcedure(stacks, steps);
        return GetMessage(stacks, stackCount);
    }
}

using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdventOfCode2022;
public static class Day07
{
    private static Dictionary<string, List<(string FileName, int Size)>> fileSystem = new();

    public static void One(string path)
    {
        string[] terminalOutput = File.ReadAllLines(path);
        //var x = terminalOutput
        //    .Select((o, i) => (Output: o, Index: i))
        //    .Where(o => o.Output.StartsWith("$ cd") && !o.Output.EndsWith(".."))
        //    .Select(o => (Directory: o.Output.Split(' ')[^1], Size: Solve(terminalOutput, 0, o.Index)));
        var results = new List<(string directory, int size)>();
        var cds = terminalOutput
            .Select((o, i) => (Output: o, Index: i))
            .Where(o => o.Output.StartsWith("$ cd") && !o.Output.EndsWith("..")).ToList();
        foreach (var cd in cds)
        {
            results.Add((cd.Output.Split(' ')[^1], Solve(terminalOutput, 0, cd.Index)));
        }
        Console.WriteLine(); 
    }

    private static int Solve(string[] terminalOutput, int level, int index)
    {
        int size = 0;
        string command = terminalOutput[index];
        if (level < 0 || index >= terminalOutput.Length)
            return size;

        if (terminalOutput[index] == "$ ls" || terminalOutput[index].StartsWith("dir"))
            size += Solve(terminalOutput, level, ++index);

        if (terminalOutput[index] == "$ cd ..")
            size += Solve(terminalOutput, --level, ++index);

        if (terminalOutput[index].StartsWith("$ cd") && terminalOutput[index].Split(' ')[^1] != "..")
            size += Solve(terminalOutput, ++level, ++index);

        if (char.IsDigit(terminalOutput[index][0]))
        {
            size += int.Parse(terminalOutput[index].Split(' ')[0]);
            size += Solve(terminalOutput, level, ++index);
        }

        return size;
    }

    private static int AddTwo(int x) => x + 2;
}

using Common;
using System.Numerics;
using System.Text.RegularExpressions;

string input = await Utils.GetInput(day: 13);

var splittedInput = input.Trim().Split("\n\n");
var points = splittedInput[0].Split('\n').Select(line =>
    new Complex(int.Parse(line.Split(',')[0]), int.Parse(line.Split(',')[1]))).ToList();
var instructions = splittedInput[1].Split('\n')
    .Select(line =>
    {
        var match = Regex.Match(line, @".*\s(.*)=(\d+)");
        var value = int.Parse(match.Groups[2].Value);

        return match.Groups[1].Value.Equals("x") ? new Complex(value, 0) : new Complex(0, value);
    })
    .ToList();

static HashSet<Complex> Fold(IReadOnlyList<Complex> points, Complex instruction)
{
    List<Complex> result = points.ToList();
    for (int i = 0; i < points.Count; i++)
    {
        if (instruction.Real == 0 && points[i].Imaginary > instruction.Imaginary)
            result[i] -= 2 * new Complex(0, points[i].Imaginary - instruction.Imaginary);

        if (instruction.Imaginary == 0 && points[i].Real > instruction.Real)
            result[i] -= 2 * new Complex(points[i].Real - instruction.Real, 0);
    }

    return new HashSet<Complex>(result);
}

Console.WriteLine(Fold(points, instructions[0]).Count);

instructions.ForEach(instruction => points = [..Fold(points, instruction)]);

points
    .OrderBy(point => point.Imaginary)
    .ThenBy(point => point.Real)
    .GroupBy(point => point.Imaginary)
    .ToList()
    .ForEach(g =>
    {
        var visibles = g.Select(point => point.Real);
        for (int i = 0; i <= g.Max(g => g.Real); i++)
            Console.Write(visibles.Contains(i) ? '■' : ' ');

        Console.WriteLine();
    });
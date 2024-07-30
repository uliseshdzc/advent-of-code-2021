using Common;
using System.Numerics;

string input = await Utils.GetInput(day: 2);

Dictionary<string, Complex> movements = new()
{
    { "forward", new(1, 0) },
    { "up", new(0, -1) },
    { "down", new(0, 1) },
};

static (string movement, int moves) ParseLine(string line)
{
    var splittedString = line.Split(' ');
    string movement = splittedString[0];
    int moves = int.Parse(splittedString[1]);

    return (movement, moves);
}

var position = input.Trim().Split('\n')
    .Select(ParseLine)
    .Aggregate(new Complex(), (total, line) => total + movements[line.movement] * line.moves);

Console.WriteLine(position.Real * position.Imaginary);

int aim = 0;
position = input.Trim().Split('\n')
    .Select(ParseLine)
    .Aggregate(new Complex(), (total, line) =>
    {
        if (line.movement.Equals("forward"))
            return total + new Complex(line.moves, aim * line.moves);

        aim += (int)movements[line.movement].Imaginary * line.moves;
        return total;
    });

Console.WriteLine(position.Real * position.Imaginary);
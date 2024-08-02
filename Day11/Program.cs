using Common;

string input = await Utils.GetInput(day: 11);

int[][] GetOctopuses() => input.Trim().Split("\n").Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

static List<(int, int)> GetPositionsAround(int i, int j) => new()
{
    (i - 1, j - 1),
    (i - 1, j),
    (i - 1, j + 1),
    (i, j - 1),
    (i, j + 1),
    (i + 1, j - 1),
    (i + 1, j),
    (i + 1, j + 1),
};

var result = 0;
var octopuses = GetOctopuses();
for (int step = 0; step < 100;  step++)
{
    var stack = new Stack<(int, int)>();
    var flashes = new HashSet<(int, int)>();

    for (int i = 0; i < octopuses.Length; i++)
        for (int j = 0; j < octopuses[i].Length; j++)
            stack.Push((i, j));

    while (stack.Count > 0)
    {
        var (i, j) = stack.Pop();

        if (i < 0 || j < 0 || i >= octopuses.Length || j >= octopuses[0].Length)
            continue;

        if (flashes.Contains((i, j)))
            continue;

        octopuses[i][j] = (octopuses[i][j] + 1) % 10;

        if (octopuses[i][j] == 0)
        {
            GetPositionsAround(i, j).ForEach(stack.Push);
            flashes.Add((i, j));
        }
    }

    result += flashes.Count;
}

Console.WriteLine(result);

result = 0;
octopuses = GetOctopuses();
while (true)
{
    var stack = new Stack<(int, int)>();
    var flashes = new HashSet<(int, int)>();

    for (int i = 0; i < octopuses.Length; i++)
        for (int j = 0; j < octopuses[i].Length; j++)
            stack.Push((i, j));

    while (stack.Count > 0)
    {
        var (i, j) = stack.Pop();

        if (i < 0 || j < 0 || i >= octopuses.Length || j >= octopuses[0].Length)
            continue;

        if (flashes.Contains((i, j)))
            continue;

        octopuses[i][j] = (octopuses[i][j] + 1) % 10;

        if (octopuses[i][j] == 0)
        {
            GetPositionsAround(i, j).ForEach(stack.Push);
            flashes.Add((i, j));
        }
    }

    result++;

    if (flashes.Count == octopuses.Length * octopuses[0].Length)
        break;
}

Console.WriteLine(result);
using Common;

string input = await Utils.GetInput(day: 5);

(int, int) GetPoint(string pointString)
{
    var numbersStrings = pointString.Split(',');
    return (int.Parse(numbersStrings[0]), int.Parse(numbersStrings[1]));
}

((int X, int Y) start, (int X, int Y) end) GetPairs(string line)
{
    var pointsStrings = line.Split(" -> ");
    return (GetPoint(pointsStrings[0]), GetPoint(pointsStrings[1]));
}

static void InitializeOrIncrease<T>(Dictionary<T, int> dictionary, T key)
{
    if (dictionary.ContainsKey(key))
        dictionary[key]++;
    else
        dictionary[key] = 1;
}

var lines = input.Trim()
    .Split('\n')
    .Select(GetPairs)
    .ToList();

var count = new Dictionary<(int, int), int>();

lines.Where(points => points.start.Y == points.end.Y)
    .ToList()
    .ForEach(line =>
    {
        for (int x = Math.Min(line.start.X, line.end.X); x <= Math.Max(line.start.X, line.end.X); x++)
            InitializeOrIncrease(count, key: (x, line.start.Y));
    });

lines.Where(points => points.start.X == points.end.X)
    .ToList()
    .ForEach(line => 
    {
        for (int y = Math.Min(line.start.Y, line.end.Y); y <= Math.Max(line.start.Y, line.end.Y); y++)
            InitializeOrIncrease(count, key: (line.start.X, y));
    });

Console.WriteLine(count.Values.Count(c => c >= 2));

lines.Where(points => Math.Abs(points.start.X - points.end.X) == Math.Abs(points.start.Y - points.end.Y))
    .ToList()
    .ForEach(line =>
    {
        var xIncrement = Math.Sign(line.end.X - line.start.X);
        var yIncrement = Math.Sign(line.end.Y - line.start.Y);
        for (int i = 0; i <= Math.Abs(line.start.X - line.end.X); i++)
            InitializeOrIncrease(count, key: (line.start.X + i * xIncrement, line.start.Y + i * yIncrement));
    });

Console.WriteLine(count.Values.Count(c => c >= 2));

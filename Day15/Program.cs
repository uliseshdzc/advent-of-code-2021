using Common;

string input = await Utils.GetInput(day: 15);

int[][] data = input.Trim().Split('\n')
    .Select(line => line
        .Select(c => int.Parse(c.ToString()))
        .ToArray())
    .ToArray();

static int? DjikstrasAlgorithm(int[][] data)
{
    var queue = new PriorityQueue<(int i, int j), int>();
    queue.Enqueue((i: 0, j: 0), priority: 0);
    var visitedPositions = new HashSet<(int i, int j)>();

    while (queue.Count > 0)
    {
        queue.TryDequeue(out (int i, int j) position, out int priority);

        if (position.i == data.Length - 1 && position.j == data[0].Length - 1)
            return priority;

        if (visitedPositions.Contains((position.i, position.j)))
            continue;

        visitedPositions.Add((position.i, position.j));
        var nextPositions = new List<(int i, int j)>
        {
            (position.i + 1, position.j),
            (position.i - 1, position.j),
            (position.i, position.j + 1),
            (position.i, position.j - 1)
        };

        foreach (var nextPosition in nextPositions)
        {
            if (nextPosition.i < 0 || nextPosition.i >= data.Length || nextPosition.j < 0 || nextPosition.j >= data[0].Length)
                continue;

            queue.Enqueue((nextPosition.i, nextPosition.j), priority + data[nextPosition.i][nextPosition.j]);
        }
    }

    return null;
}

Console.WriteLine(DjikstrasAlgorithm(data));

const int Increment = 5;
var originalRows = data.Length;
var originalCols = data[0].Length;

int[][] newData = new int[originalRows * Increment][];
for (int i = 0; i < newData.Length; i++)
    newData[i] = new int[originalCols * Increment];

for (int i = 0; i < originalRows * Increment; i++)
    for (int j = 0; j < originalCols * Increment; j++)
        newData[i][j] = (data[i % originalRows][j % originalCols] + i / originalRows + j / originalCols - 1) % 9 + 1;

Console.WriteLine(DjikstrasAlgorithm(newData));

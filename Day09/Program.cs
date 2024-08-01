using Common;

string input = await Utils.GetInput(day: 9);

var heightmap = input.Trim().Split('\n');

static List<(int i, int j)> PointsAround(int i, int j) => new()
{
    (i - 1, j),
    (i + 1, j),
    (i, j - 1),
    (i, j + 1),
};

bool IsInBounds(int i, int j) =>
    i >= 0 &&
    i < heightmap.Length &&
    j >= 0 &&
    j < heightmap[i].Length;

int result = 0;
var lowPoints = new HashSet<(int i, int j)>(); // Used in 2nd part
for (int i = 0; i < heightmap.Length; i++) 
{
    for (int j = 0; j < heightmap[i].Length; j++)
    {
        bool isMinimum = true;
        foreach (var pointAround in PointsAround(i, j))
        {
            if (IsInBounds(pointAround.i, pointAround.j) && 
                heightmap[pointAround.i][pointAround.j] <= heightmap[i][j])
            {
                isMinimum = false;
                break;
            }
        }

        if (isMinimum)
        {
            result += 1 + int.Parse(heightmap[i][j].ToString());
            lowPoints.Add((i, j));
        }
    }
}

Console.WriteLine(result);

var seen = new HashSet<(int i, int j)>();
void Dfs(int i, int j)
{
    if (!IsInBounds(i, j)) return;
    if (heightmap[i][j] == '9') return;
    if (seen.Contains((i, j))) return;

    seen.Add((i, j));
    foreach (var pointAround in PointsAround(i, j))
        Dfs(pointAround.i, pointAround.j);
}

var sizes = new List<int>();
foreach (var (i, j) in lowPoints) // Each low point corresponds to a bassin
{
    Dfs(i, j);
    sizes.Add(seen.Count);
    seen.Clear();
}

Console.WriteLine(sizes
    .OrderDescending()
    .Take(3)
    .Aggregate((a, b) => a * b));
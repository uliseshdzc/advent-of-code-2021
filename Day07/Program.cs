using Common;

string input = await Utils.GetInput(day: 7);

List<int> positions = input.Split(',').Select(int.Parse).ToList();

var minDistance = int.MaxValue;
for (int i = positions.Min(); i <= positions.Max(); i++)
{
    minDistance = Math.Min(
        positions.Select(c => Math.Abs(c - i)).Sum(),
        minDistance);

}

Console.WriteLine(minDistance);

static int ConsecutiveSum(int x) => x * (x + 1) / 2;

minDistance = int.MaxValue;
for (int i = positions.Min(); i <= positions.Max(); i++)
{
    minDistance = Math.Min(
        positions.Select(c => ConsecutiveSum(Math.Abs(c - i))).Sum(),
        minDistance);
}


Console.WriteLine(minDistance);
using Common;

string input = await Utils.GetInput(day: 12);

var nodes = new Dictionary<string, List<string>>();
input.Trim().Split('\n').ToList().ForEach(line =>
{
    var lineSplitted = line.Split('-');
    var origin = lineSplitted[0];
    var target = lineSplitted[1];

    if (!nodes.ContainsKey(origin))
        nodes.Add(origin, new());

    if (!nodes.ContainsKey(target))
        nodes.Add(target, new());

    if (!origin.Equals("start") && !target.Equals("end"))
        nodes[target].Add(origin);
    
    nodes[origin].Add(target);
});

void ExplorePath(List<string> currentPath, HashSet<List<string>> visitedPaths)
{
    if (currentPath[^1].Equals("end"))
    {
        visitedPaths.Add(currentPath);
        return;
    }

    if (currentPath[^1].All(char.IsLower) && currentPath.SkipLast(1).Contains(currentPath[^1]))
        return;

    nodes[currentPath[^1]].ForEach(target => ExplorePath([..currentPath, target], visitedPaths));
}

var visitedPaths = new HashSet<List<string>>();
ExplorePath(["start"], visitedPaths);

Console.WriteLine(visitedPaths.Count);

void ExplorePathWithMultipleVisits(List<string> currentPath, HashSet<List<string>> visitedPaths)
{
    if (currentPath[^1].Equals("end"))
    {
        visitedPaths.Add(currentPath);
        return;
    }

    if (currentPath[^1].Equals("start") && currentPath.SkipLast(1).Contains(currentPath[^1]))
        return;

    if (currentPath[^1].All(char.IsLower) && (
            currentPath.Count(node => node.Equals(currentPath[^1])) == 3 ||
            currentPath.Where(node => node.All(char.IsLower)).GroupBy(node => node).Count(g => g.Count() == 2) > 1))
        return;

    nodes[currentPath[^1]].ForEach(target => ExplorePathWithMultipleVisits([.. currentPath, target], visitedPaths));
}

visitedPaths = new HashSet<List<string>>();
ExplorePathWithMultipleVisits(["start"], visitedPaths);

Console.WriteLine(visitedPaths.Count);
using Common;

string input = await Utils.GetInput(day: 14);

var inputSplitted = input.Trim().Split("\n\n");
var polymer = inputSplitted[0];
var insertionFormulas = inputSplitted[1].Split('\n')
    .Select(line => (line.Split(" -> ")[0], line.Split(" -> ")[1]))
    .ToDictionary(line => line.Item1, line => line.Item2);

for (int step = 0; step < 10; step++)
{
    var count = polymer.Count();
    var bufferPolymer = polymer;
    for (int c = 0; c < count - 1; c++)
    {
        bufferPolymer = bufferPolymer.Insert(2 * c + 1, insertionFormulas[polymer[c..(c + 2)]]);
    }
    polymer = bufferPolymer;
}

var orderedChars = polymer.GroupBy(c => c).OrderBy(g => g.Count());

Console.WriteLine(orderedChars.Last().Count() - orderedChars.First().Count());

static void AddOrIncrement<T>(Dictionary<T, long> dictionary, T key, long increment)
{
    if (!dictionary.ContainsKey(key))
        dictionary.Add(key, 0);

    dictionary[key] += increment;
}

polymer = inputSplitted[0];
var tokensCount = new Dictionary<string, long>();
polymer
    .Zip(polymer.Skip(1))
    .ToList()
    .ForEach(pair => 
        AddOrIncrement(tokensCount, pair.First.ToString() + pair.Second.ToString(), increment: 1));

for (int step = 0; step < 40; step++)
{
    var newTokensCount = new Dictionary<string, long>();
    foreach (var startToken in tokensCount.Keys)
    {
        List<string> tokens = [
            startToken[0] + insertionFormulas[startToken],
            insertionFormulas[startToken] + startToken[1]];

        tokens.ForEach(token => AddOrIncrement(newTokensCount, token, increment: tokensCount[startToken]));
    }

    tokensCount = newTokensCount;
}

var firstLettersCount = tokensCount
    .GroupBy(x => x.Key[0])
    .ToDictionary(g => g.Key, g => g.Sum(x => x.Value))
    .OrderByDescending(kvp => kvp.Value);
var secondLettersCount = tokensCount
    .GroupBy(x => x.Key[1])
    .ToDictionary(g => g.Key, g => g.Sum(x => x.Value))
    .OrderByDescending(kvp => kvp.Value);

var result = Math.Max(firstLettersCount.First().Value, secondLettersCount.First().Value) -
    Math.Max(firstLettersCount.Last().Value, secondLettersCount.Last().Value);

Console.WriteLine(result);
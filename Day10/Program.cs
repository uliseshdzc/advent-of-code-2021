using Common;

string input = await Utils.GetInput(day: 10);

var chunks = input.Trim().Split('\n');
var incompleteChunksStacks = new List<Stack<char>>();

var characters = new Dictionary<char, char>
{
    { '(', ')' },
    { '[', ']' },
    { '{', '}' },
    { '<', '>' }
};

var charValues = new Dictionary<char, long>
{
    {')', 3 },
    {']', 57 },
    {'}', 1197 },
    {'>', 25137 }
};

long result = chunks.Aggregate(0L, (totalPoints, chunk) =>
{
    var stack = new Stack<char>();
    long points = 0;

    foreach (var c in chunk)
    {
        if (characters.ContainsKey(c))
        {
            stack.Push(c);
            continue;
        }

        if (characters[stack.Pop()] != c)
        {
            points = charValues[c];
            break;
        }
    }

    if (points == 0) 
        incompleteChunksStacks.Add(stack);

    return totalPoints + points;
});

Console.WriteLine(result);

charValues = new Dictionary<char, long>
{
    {')', 1 },
    {']', 2 },
    {'}', 3 },
    {'>', 4 }
};

var scores = incompleteChunksStacks
    .Select(chunkStack =>
    {
        long points = 0;
        while (chunkStack.Count > 0)
            points = points * 5 + charValues[characters[chunkStack.Pop()]];

        return points;
    })
    .Order()
    .ToList();

Console.WriteLine(scores[scores.Count / 2]);
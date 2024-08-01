using Combinatorics.Collections;
using Common;

string text = await Utils.GetInput(day: 8);

var outputsLines = text.Trim().Split('\n').Select(line => line.Split(" | ")[1].Split(" ")).ToList();
var inputsLines  = text.Trim().Split('\n').Select(line => line.Split(" | ")[0].Split(" ")).ToList();

var result = outputsLines
    .Sum(line => line
        .Count(output => output.Length == 2 || output.Length == 3 || output.Length == 4 || output.Length == 7));

Console.WriteLine(result);

static string Sort(string s) => new(s.Order().ToArray());

var originalCoding = new Dictionary<string, string>
{
    { Sort("acedgfb"), "8" },
    { Sort("cdfbe"),   "5" },
    { Sort("gcdfa"),   "2" },
    { Sort("fbcad"),   "3" },
    { Sort("dab"),     "7" },
    { Sort("cefabd"),  "9" },
    { Sort("cdfgeb"),  "6" },
    { Sort("eafb"),    "4" },
    { Sort("cagedb"),  "0" },
    { Sort("ab"),      "1" }
};

static string Map(string s, Dictionary<char, char> map) => new(s.Select(c => map[c]).ToArray());

var permutations = new Permutations<char>("abcdefg");
var maps = permutations.Select(p => p
    .Select((c, i) => new { Key = c, Value = "abcdefg"[i]})
    .ToDictionary(x => x.Key, x => x.Value));

result = 0;
foreach (var (inputs, outputs) in inputsLines.Zip(outputsLines))
{
    foreach(var map in maps)
    {
        var possibleCoding = inputs.Select(input => Map(input, map));
        if (possibleCoding.All(transformedInput => originalCoding.ContainsKey(Sort(transformedInput))))
        {
            var outputsDecodedList = outputs
                .Select(output => Map(output, map))
                .Select(s => originalCoding[Sort(s)]);
            var outputDecoded = string.Join(null, outputsDecodedList);
            result += int.Parse(new string(outputDecoded));
        }
    }
}

Console.WriteLine(result);
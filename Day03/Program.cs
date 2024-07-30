using Common;

string input = await Utils.GetInput(day: 3);

var report = input.Trim().Split('\n')
    .Select(line => line.ToCharArray())
    .ToArray();

static char[] GetBinaryRate(char[][] report, bool mostCommon)
    => Enumerable.Range(0, report.FirstOrDefault()?.Length ?? 0)
    .Select(i => report.Select(line => line[i]))
    .Select(col => col
        .GroupBy(c => c)
        .OrderByDescending(g => g.Count())
        .Skip(mostCommon ? 0 : 1)
        .First().Key)
    .ToArray();

static int BinaryToInt(char[] binary)
    => Convert.ToInt32(new string(binary), 2);

char[] gammaBinaryRate = GetBinaryRate(report, mostCommon: true);
char[] epsilonBinaryRate = GetBinaryRate(report, mostCommon: false);

int gamma = BinaryToInt(gammaBinaryRate);
int epsilon = BinaryToInt(epsilonBinaryRate);

Console.WriteLine(gamma * epsilon);

static char[] GetRating(char[][] report, bool mostCommon = true)
{
    return Enumerable.Range(0, report.FirstOrDefault()?.Length ?? 0)
        .Aggregate(new List<char[]>(report), (result, i) =>
        {
            if (result.Count == 1)
                return result;

            var orderedByCount = result
                .GroupBy(line => line[i])
                .OrderByDescending(g => g.Count());

            char bitCriteria = orderedByCount
                .Skip(mostCommon ? 0 : 1)
                .First().Key; 

            if (orderedByCount.DistinctBy(g => g.Count()).Count() == 1)
                bitCriteria = mostCommon ? '1' : '0';

            return result.Where(line => line[i] == bitCriteria).ToList();
        })
        .AsEnumerable()
        .First();
}

int oxygenRating = BinaryToInt(GetRating(report, mostCommon: true));
int co2Rating = BinaryToInt(GetRating(report, mostCommon: false));

Console.WriteLine(oxygenRating * co2Rating);
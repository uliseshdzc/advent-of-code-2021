using Common;

static int CountMeasurementIncreases(IEnumerable<int> measurements)
    => measurements
    .Skip(1)
    .Zip(measurements, (a, b) => a > b)
    .Count(x => x);

string input = await Utils.GetInput(day: 1);

var measurements = input.Trim()
    .Split("\n")
    .Select(int.Parse)
    .ToList();

var firstResult = CountMeasurementIncreases(measurements);
Console.WriteLine(firstResult);

var slidingWindowMeasurements = Enumerable
    .Range(0, measurements.Count - 2)
    .Select(i => measurements[i] + measurements[i + 1] + measurements[i + 2]);

var secondResult = CountMeasurementIncreases(slidingWindowMeasurements);
Console.WriteLine(secondResult);
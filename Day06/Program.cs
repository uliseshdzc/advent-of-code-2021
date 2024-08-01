using Common;

string input = await Utils.GetInput(day: 6);

List<int> GetStates() => input.Split(',').Select(int.Parse).ToList();

List<int> states = GetStates();
for (int i = 0; i < 80; i++)
{
    int count = states.Count;
    for (int s = 0; s < count; s++)
    {
        if (states[s] == 0)
        {
            states[s] = 6;
            states.Add(8);
            continue;
        }

        states[s]--;
    }
}

Console.WriteLine(states.Count);

states = GetStates();
const int PossibleAges = 9;
int zeroIndex = 0;

var fishCount = new long[PossibleAges];
Enumerable.Range(0, states.Count).ToList().ForEach(s => fishCount[states[s]]++);

for (int i = 0; i < 256; i++)
{
    fishCount[(zeroIndex + 7) % PossibleAges] += fishCount[zeroIndex];
    zeroIndex = (zeroIndex + 1) % PossibleAges;
}

Console.WriteLine(fishCount.Sum());
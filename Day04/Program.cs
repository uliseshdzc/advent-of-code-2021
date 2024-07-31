using Common;

string input = await Utils.GetInput(day: 4);

List<int> numbers = input
    .Split('\n')[0]
    .Split(',')
    .Select(int.Parse)
    .ToList();

List<int?[][]> boards = input.Trim()
    .Split("\n\n")
    .Skip(1)
    .Select(board => board
        .Split('\n')
        .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => (int?)int.Parse(x)).ToArray())
        .ToArray())
    .ToList();

static List<int?[][]> FindAndReplace(List<int?[][]> boards, int value)
{
    for (int b = 0; boards.Count > b; b++)
    {
        bool found = false;

        for (int i = 0; i < boards[b].Length; i++) 
            for (int j = 0; j < boards[b][i].Length; j++)
            {
                if (boards[b][i][j] == value)
                {
                    boards[b][i][j] = null;
                    found = true;
                    break;
                }

                if (found) break;
            }
    }

    return boards;
}

static bool CheckIfBoardIsWinner(int?[][] board)
{
    // Check rows
    for (int i = 0; i < board.Length; i++)
        if (board[i].Distinct().Count() == 1)
            return true;

    // Check columns
    for (int j = 0; j < board[0].Length; j++)
        if (board.Select(row => row[j]).Distinct().Count() == 1)
            return true;

    return false;
}

static int CalculateBoardValue(int?[][] board, int lastNumber)
{
    var result = 0;
    for (int i = 0; i < board.Length; i++)
        for (int j = 0; j < board.Length; j++)
            result += board[i][j] ?? 0;

    return result * lastNumber;
}

int? score = null;
foreach (var number in numbers)
{
    boards = FindAndReplace(boards, number);

    for (int b = 0; b < boards.Count; b++)
        if (CheckIfBoardIsWinner(boards[b]))
        {
            score = CalculateBoardValue(boards[b], number);
            break;
        }

    if (score != null) break;
}

Console.WriteLine(score);

score = null;
foreach (var number in numbers)
{
    boards = FindAndReplace(boards, number);

    for (int b = 0; b < boards.Count; b++)
        if (CheckIfBoardIsWinner(boards[b]))
        {
            score = CalculateBoardValue(boards[b], number);
            boards.RemoveAt(b);
        }
}

Console.WriteLine(score);

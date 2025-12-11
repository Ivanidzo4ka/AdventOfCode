var _dp = new Dictionary<long, int>();
PartOne(true);
PartOne(false);
PartTwo(true);
PartTwo(false);
void PartOne(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
    {
        Console.Write("Test:");
    }
    foreach (var puzzle in data)
    {
        ans += Solve(puzzle);
    }
    Console.WriteLine(ans);
}
int Solve(Puzzle puzzle)
{
    var queue = new Queue<int>();
    queue.Enqueue(puzzle.Lights);
    var seen = new HashSet<int>();
    seen.Add(puzzle.Lights);
    var step = 0;
    while (true)
    {

        var newQueue = new Queue<int>();
        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            if (state == 0)
            {
                return step;
            }
            foreach (var btn in puzzle.Buttons)
            {
                var newState = state;
                foreach (var idx in btn)
                {
                    newState ^= (1 << idx);
                }
                if (!seen.Contains(newState))
                {
                    seen.Add(newState);
                    newQueue.Enqueue(newState);
                }
            }
        }
        step++;
        (queue, newQueue) = (newQueue, queue);
    }
}
int SolveJoltage(IList<int> joltage, IList<HashSet<int>> buttons)
{
    var key = 0L;
    for (int i = 0; i < joltage.Count; i++)
    {
        key *= 256;
        key += joltage[i];
    }

    if (_dp.ContainsKey(key))
    {
        return _dp[key];
    }
    var ans = int.MaxValue;
    var min = -1;
    for (int i = 0; i < joltage.Count; i++)
    {
        if ((min == -1 || joltage[i] < joltage[min]) && joltage[i] > 0)
        {
            min = i;
        }
    }
    for (int i = 0; i < buttons.Count; i++)
    {
        if (buttons[i].Contains(min))
        {
            var newJoltage = new List<int>(joltage);
            var good = true;
            foreach (var idx in buttons[i])
            {
                if (joltage[idx] < joltage[min])
                {
                    good = false;
                    break;
                }
                newJoltage[idx] = newJoltage[idx] - joltage[min];
            }
            if (!good) continue;
            var solve = SolveJoltage(newJoltage, buttons);
            if (solve == int.MaxValue)
            {
                continue;
            }
            else ans = Math.Min(ans, joltage[min] + solve);
        }
    }
    _dp[key] = ans;
    return _dp[key];
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
    {
        Console.Write("Test:");
    }
    var t = new int[4] { 31, 4, 31, 29 };


    for (int a = 0; a <= 31; a++)
        for (int b = 0; b <= 31; b++)
            for (int c = 0; c <= 31; c++)
                for (int d = 0; d <= 31; d++)
                    for (int e = 0; e <= 31; e++)
                    {
                        if (a + d + e == 31 && b + d == 4 && a + c + d == 31 && a + b + c == 29)
                        {
                            ans = a + b + c + d + e;
                        }
                    }
    foreach (var puzzle in data)
    {
        _dp.Clear();
        _dp[0] = 0;
        var buttons = new List<HashSet<int>>();
        foreach (var btn in puzzle.Buttons)
        {
            buttons.Add(new HashSet<int>(btn));
        }
        ans += SolveJoltage(puzzle.Joltage, buttons);
    }
    Console.WriteLine(ans);
}

IList<Puzzle> ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var data = new List<Puzzle>();
    foreach (var line in lines)
    {
        var puzzle = new Puzzle();
        var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var c = 0;
        for (int i = 1; i < split[0].Length - 1; i++)
        {
            if (split[0][i] == '#')
            {
                c += (1 << (i - 1));
            }
        }
        puzzle.Lights = c;
        puzzle.Buttons = new List<IList<int>>();
        for (int i = 1; i < split.Length - 1; i++)
        {
            var btn = new List<int>();
            split[i][1..^1].Split(',', StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(x => btn.Add(int.Parse(x)));
            puzzle.Buttons.Add(btn);
        }
        puzzle.Joltage = new List<int>();
        split[^1][1..^1].Split(',', StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(x => puzzle.Joltage.Add(int.Parse(x)));

        data.Add(puzzle);
    }
    return data;
}

public class Puzzle
{
    public int Lights;
    public IList<IList<int>> Buttons;
    public IList<int> Joltage;

    public long Jolt;

}
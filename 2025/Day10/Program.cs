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
        ans += Solve(puzzle.Lights, puzzle.Buttons);
    }
    Console.WriteLine(ans);
}
int Solve(int lights, IList<IList<int>> buttons)
{
    var queue = new Queue<int>();
    queue.Enqueue(lights);
    var seen = new HashSet<int>
    {
        lights
    };
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
            foreach (var btn in buttons)
            {
                var newState = state;
                foreach (var idx in btn)
                {
                    newState ^= 1 << idx;
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
int SolveJoltage(IList<int> joltage, IList<IList<int>> buttons)
{
    var n = joltage.Count;
    var m = buttons.Count;
    var matrix = new double[n][];
    for (int i = 0; i < n; i++)
    {
        matrix[i] = new double[m + 1];
        matrix[i][m] = joltage[i];
    }
    for (int i = 0; i < m; i++)
    {
        foreach (var idx in buttons[i])
        {
            matrix[idx][i] = 1;
        }
    }
    ProcessMatrix(matrix);
    matrix = PruneMatrix(matrix);
    //ProcessMatrix(matrix);

    var ans = SolveMatrix(matrix, m - 1, new int[m]);
    Console.WriteLine(ans);
    return ans;
}
double[][] PruneMatrix(double[][] matrix)
{
    List<double[]> result = new List<double[]>();
    for (int i = 0; i < matrix.Length; i++)
    {
        var allZero = true;
        for (int j = 0; j < matrix[0].Length-1; j++)
        {
            matrix[i][j] = Math.Round(matrix[i][j], 6);
            if (matrix[i][j] != 0)
            {
                allZero = false;
            }

        }
        if (!allZero) result.Add(matrix[i]);
    }
    return result.ToArray();

}
void ProcessMatrix(double[][] matrix)
{
    var n = matrix.Length;
    var m = matrix[0].Length - 1;
    for (int i = 0; i < n; i++)
    {
        if (matrix[i][i] == 0)
        {
            var change = i + 1;
            while (change < n && matrix[change][i] == 0)
                change++;
            if (change == n)
            {
                change = i + 1;
                while (change < m && matrix[i][change] == 0)
                    change++;
                if (change >= m)
                    return;
                for (int z = 0; z < n; z++)
                {
                    (matrix[z][i], matrix[z][change]) = (matrix[z][change], matrix[z][i]);
                }
            }
            else
            {
                for (int j = 0; j < m + 1; j++)
                {
                    matrix[i][j] += matrix[change][j] / matrix[change][i];
                }
            }
        }

        if (matrix[i][i] != 1)
        {
            var t = matrix[i][i];
            for (int j = 0; j < m + 1; j++)
            {
                matrix[i][j] = matrix[i][j] / t;
            }
        }
        for (int r = i + 1; r < n; r++)
        {

            var mul = matrix[r][i];
            if (mul == 0)
                continue;
            for (int j = i; j < m + 1; j++)
            {
                matrix[r][j] = matrix[r][j] - mul * matrix[i][j];
            }
        }
        matrix = PruneMatrix(matrix);
        n = matrix.Length;
        PrintMatrix(matrix);
    }
}
int SolveMatrix(double[][] matrix, int pos, int[] ans)
{
    var res = 1000000;
    if (pos == -1)
    {
        return ans.Sum();
    }
    if (matrix.Length > pos)
    {
        double cand = 0;
        for (int j = matrix[0].Length - 2; j > pos; j--)
        {
            cand += matrix[pos][j] * ans[j];
        }
        cand = matrix[pos][^1] - cand;
        var intCand = (int)Math.Round(cand);
        if (Math.Abs(cand - intCand) < 1e-4 && intCand >= 0)
        {
            ans[pos] = intCand;
            res = Math.Min(res, SolveMatrix(matrix, pos - 1, ans));
        }
    }
    else
    {
        for (int i = 0; i < 256; i++)
        {
            ans[pos] = i;
            res = Math.Min(res, SolveMatrix(matrix, pos - 1, ans));
        }
    }
    return res;
}
void PrintMatrix(double[][] matrix)
{
    return;
    for (int i = 0; i < matrix.Length; i++)
    {
        for (int j = 0; j < matrix[i].Length; j++)
        {
            Console.Write($"{matrix[i][j],8:F2} ");
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
    {
        Console.Write("Test:");
    }
    foreach (var puzzle in data)
    {
        var t = SolveJoltage(puzzle.Joltage, puzzle.Buttons);
        ans += t;
        if (t == 1000000)
        {
            Console.WriteLine(string.Join(",", puzzle.Joltage));
        }

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

}
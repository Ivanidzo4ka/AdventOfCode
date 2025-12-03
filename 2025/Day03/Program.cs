
long[] _mul = new long[13];
Dictionary<(int x, int y), long> _dp = new();

PartOne(true);
PartOne(false);
PartTwo(true);
PartTwo(false);
void PartOne(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    foreach (var line in data)
    {
        ans += MaxJoltage(line);
    }
    Console.WriteLine(ans);
}
int MaxJoltage(string line)
{
    var max = new int[line.Length];
    max[^1] = line[^1] - '0';
    for (int i = line.Length - 2; i >= 0; i--)
    {
        max[i] = Math.Max(line[i] - '0', max[i + 1]);
    }
    var ans = 0;
    for (int i = 0; i < line.Length - 1; i++)
    {
        ans = Math.Max(ans, 10 * (line[i] - '0') + max[i + 1]);
    }
    return ans;
}
long MaxJoltageDp(string line, int start, int left)
{
    if (line.Length - start <= left) return -1;
    if (left == -1) return 0;
    var ans = 0L;
    if (_dp.ContainsKey((start, left)))
        return _dp[(start, left)];

    for (int i = start; i < line.Length-left; i++)
    {
        ans = Math.Max(ans, (line[i] - '0') * _mul[left] + MaxJoltageDp(line, i + 1, left - 1));
    }
    _dp[(start, left)] = ans;
    return ans;
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    _mul[0] = 1;
    for (int i = 1; i < _mul.Length; i++)
    {
        _mul[i] = _mul[i - 1] * 10;
    }
    foreach (var line in data)
    {
        _dp.Clear();
        ans += MaxJoltageDp(line, 0, 11);
    }
    Console.WriteLine(ans);
}
string[] ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");

    return lines;
}
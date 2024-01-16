

using System.Reflection.Metadata;

PartOne();
PartTwo();

void PartOne()
{
    var ans = 0L;
    var data = Parse("input.txt");
    ans = Solve(data, 150);
    Console.WriteLine(ans);
}
long Solve(List<int> arr, int limit)
{
    var dp = new long[limit + 1];
    dp[0] = 1;
    for (int i = 0; i < arr.Count; i++)
    {
        for (int a = limit - arr[i]; a >= 0; a--)
            if (dp[a] != 0)
            {
                dp[a + arr[i]] += dp[a];
            }
    }
    return dp[limit];
}
var _min = int.MaxValue;
var _count = 0;
void PartTwo()
{
    var ans = 0L;
    _min = int.MaxValue;
    _count = 0;
    var data = Parse("input.txt");
    Solve2(data, 0, 0, 150);
    Console.WriteLine(_count);

}
void Solve2(List<int> arr, int pos, int used, int limit)
{
    if (limit == 0)
    {
        if (_min > used)
        {
            _min = used;
            _count = 1;
        }
        else if (_min == used)
        {
            _count++;
        }
        return;
    }
    else if (limit < 0 || pos == arr.Count)
        return;
    Solve2(arr, pos + 1, used, limit);
    Solve2(arr, pos + 1, used + 1, limit - arr[pos]);
}
List<int> Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    var ans = new List<int>();
    foreach (var line in lines)
        ans.Add(int.Parse(line));
    ans.Sort();
    return ans;
}

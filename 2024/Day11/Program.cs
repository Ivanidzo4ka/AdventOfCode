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
    for (int i = 0; i < 25; i++)
        Blink(data);
    Console.WriteLine(data.Count);
}

void Blink(LinkedList<long> stones)
{
    var cur = stones.First;
    while (cur != null)
    {
        var next = cur.Next;
        if (cur.Value == 0) { cur.Value++; cur = next; continue; }
        var s = cur.Value.ToString();
        if (s.Length % 2 == 0)
        {
            var left = long.Parse(s.Substring(0, s.Length / 2));
            var right = long.Parse(s.Substring(s.Length / 2, s.Length / 2));
            cur.Value = right;
            stones.AddBefore(cur, left);
        }
        else cur.Value *= 2024;
        cur = next;
    }
}
void Blink2(ref Dictionary<long, long> stones)
{
    var newOne = new Dictionary<long, long>();
    foreach (var cur in stones)
    {
        if (cur.Key == 0)
        {
            newOne.TryGetValue(1, out var nextCount);
            newOne[1] = nextCount + cur.Value;
        }
        else
        {
            var s = cur.Key.ToString();
            if (s.Length % 2 == 0)
            {
                var left = long.Parse(s.Substring(0, s.Length / 2));
                var right = long.Parse(s.Substring(s.Length / 2, s.Length / 2));
                newOne.TryGetValue(left, out var nextCount);
                newOne[left] = nextCount + cur.Value;
                newOne.TryGetValue(right, out nextCount);
                newOne[right] = nextCount + cur.Value;
            }
            else
            {
                newOne.TryGetValue(cur.Key * 2024, out var nextCount);
                newOne[cur.Key * 2024] = nextCount + cur.Value;
            }
        }

    }
    stones = newOne;
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);

    var ans = 0L;
    if (test)
        Console.Write("Test:");
    var dic = new Dictionary<long, long>();
    foreach (var elem in data)
    {
        dic.TryGetValue(elem, out var c);
        dic[elem] = c + 1;
    }
    for (int i = 0; i < 75; i++)
        Blink2(ref dic);
    foreach (var kvp in dic)
    {
        ans += kvp.Value;
    }
    Console.WriteLine(ans);
}


LinkedList<long> ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var result = new LinkedList<long>();
    foreach (var stone in lines[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)))
        result.AddLast(stone);
    return result;


}
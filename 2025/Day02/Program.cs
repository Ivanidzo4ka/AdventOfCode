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
    foreach (var range in data)
    {
        for (long i = range.x; i <= range.y; i++)
        {
            var t = i.ToString();

            if (t.Length % 2 == 0)
            {
                if (t[..(t.Length / 2)] == t[(t.Length / 2)..])
                    ans += i;
            }
        }
    }
    Console.WriteLine(ans);
}

void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    foreach (var range in data)
    {
        for (long i = range.x; i <= range.y; i++)
        {
            var t = i.ToString();
            for (int z = 1; z <= t.Length / 2; z++)
                if (t.Length % z == 0)
                {
                    var parts = t.Length/z ;
                    var good =true;
                    for (int j=1; j< parts; j++)
                        if (t[..z]!= t[(j*z)..(j*z+z)])
                        {
                            good=false;
                            break;
                        }
                    if (good) {
                        ans+=i; break;
                        }
                }
        }
    }
    Console.WriteLine(ans);
}
IList<(long x, long y)> ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var data = new List<(long x, long y)>();
    foreach (var line in lines[0].Split(","))
    {
        data.Add((long.Parse(line.Split("-")[0]), long.Parse(line.Split("-")[1])));
    }
    return data;
}
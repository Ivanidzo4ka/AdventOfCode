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
    foreach (var set in data)
    {
        var min = int.MaxValue;
        for (int a = 0; a < 100; a++)
            for (int b = 0; b < 100; b++)
            {
                if (a * set[0] + b * set[2] == set[4] && a * set[1] + b * set[3] == set[5])
                {
                    min = Math.Min(min, a * 3 + b);
                }
            }
        if (min != int.MaxValue)
            ans += min;
    }
    Console.WriteLine(ans);
}

void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");

    foreach (var set in data)
    {
        var min = long.MaxValue;
        long X = 10000000000000 + set[4];
        long Y = 10000000000000 + set[5];
        long x1 = set[0];
        long x2 = set[2];
        long y1 = set[1];
        long y2 = set[3];
        var dif = X * y1 - Y * x1;
        var big = x2 * y1 - y2 * x1;
        if (dif % big == 0)
        {
            var b = dif / big;
            if ((X - b * x2) % x1 == 0)
            {
                var a = (X - b * x2) / x1;
                min = Math.Min(min, a * 3 + b);
            }
        }
        if (min != long.MaxValue)
            ans += min;

        /*
        a* x1 + b*x2 = X
        a* y1 + b*y2 = Y;
        a*x1*y1 +b*x2*y1 = X*y1;
        a*x1*y1 + b*y2*x1 = Y*x1;
        b*x2*y1-b*y2*x1 = X*y1*x1
        b(x2*y1-y2*x1)
        */
    }
    Console.WriteLine(ans);
}


List<int[]> ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var result = new List<int[]>();
    for (int i = 0; i < lines.Length; i += 4)
    {
        var set = new int[6];
        var comma = lines[i].Split(',');
        set[0] = int.Parse(comma[0].Split('+')[1]);
        set[1] = int.Parse(comma[1].Split('+')[1]);
        comma = lines[i + 1].Split(',');
        set[2] = int.Parse(comma[0].Split('+')[1]);
        set[3] = int.Parse(comma[1].Split('+')[1]);
        comma = lines[i + 2].Split(',');
        set[4] = int.Parse(comma[0].Split('=')[1]);
        set[5] = int.Parse(comma[1].Split('=')[1]);
        result.Add(set);
    }
    return result;
    //return lines.Select(x => x.ToCharArray()).ToArray();
}
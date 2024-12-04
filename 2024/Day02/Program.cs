PartOne(true);
PartOne(false);
PartTwo(true);
PartTwo(false);
void PartOne(bool test = false)
{
    var data = ReadData(test);
    var ans = 0;
    if (test)
        Console.Write("Test:");
    foreach (var report in data)
    {
        if (Safe(report)) ans++;
    }
    Console.WriteLine(ans);
}
bool Safe(int[] report)
{
    bool inc = true;
    bool dec = true;
    var diff = true;
    for (int i = 0; i < report.Length - 1; i++)
    {
        if (report[i] >= report[i + 1]) dec = false;
        if (report[i] <= report[i + 1]) inc = false;
        if (Math.Abs(report[i] - report[i + 1]) > 3) diff = false;
    }
    return (inc || dec) && diff;
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0;
    if (test)
        Console.Write("Test:");
    foreach (var report in data)
    {
        if (Safe(report)) ans++;
        else
        {
            for (int i = 0; i < report.Length; i++)
            {
                var t = report.ToList();
                t.RemoveAt(i);
                if (Safe(t.ToArray())) { ans++; break; }
            }
        }
    }
    Console.WriteLine(ans);
}

List<int[]> ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var result = new List<int[]>();
    foreach (var line in lines)
    {
        result.Add(line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray());
    }
    return result;
}
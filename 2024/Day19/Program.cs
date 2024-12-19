using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Intrinsics.Arm;

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
    foreach (var towel in data.rest)
    {
        if (CanSolve(towel, data.data))
            ans++;
    }
    Console.WriteLine(ans);
}
bool CanSolve(string towel, Dictionary<char, List<string>> data)
{
    bool[] can = new bool[towel.Length + 1];
    can[0] = true;
    for (int i = 0; i < towel.Length; i++)
    {
        if (can[i])
        {
            if (data.TryGetValue(towel[i], out var patterns))
            {
                foreach (var pattern in patterns)
                {
                    if (towel.Length - i >= pattern.Length)
                    {
                        if (towel.Substring(i, pattern.Length) == pattern)
                        {
                            can[i + pattern.Length] = true;

                        }
                    }
                }
            }
        }
    }
    return can[towel.Length];
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    foreach (var towel in data.rest)
    {
        ans += (Count(towel, data.data));
    }

    Console.WriteLine(ans);
}
long Count(string towel, Dictionary<char, List<string>> data)
{
    long[] can = new long[towel.Length + 1];
    can[0] = 1;
    for (int i = 0; i < towel.Length; i++)
    {
        if (can[i] != 0)
        {
            if (data.TryGetValue(towel[i], out var patterns))
            {
                foreach (var pattern in patterns)
                {
                    if (towel.Length - i >= pattern.Length)
                    {
                        if (towel.Substring(i, pattern.Length) == pattern)
                        {
                            can[i + pattern.Length] += can[i];

                        }
                    }
                }
            }
        }
    }
    return can[towel.Length];
}


(Dictionary<char, List<string>> data, List<string> rest) ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var dic = new Dictionary<char, List<string>>();

    foreach (var line in lines[0].Split(", ", StringSplitOptions.RemoveEmptyEntries))
    {
        if (!dic.TryGetValue(line[0], out var l))
        {
            l = new List<string>();
            dic[line[0]] = l;
        }
        l.Add(line);

    }
    return (dic, lines.Skip(2).ToList());
}
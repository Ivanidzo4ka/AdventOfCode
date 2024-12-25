PartOne(true);
PartOne(false);

void PartOne(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    for (int i = 0; i < data.locks.Count; i++)
    {
        for (int j = 0; j < data.keys.Count; j++)
        {
            var match = true;
            for (int z = 0; z < data.locks[i].Length; z++)
            {
                if (data.locks[i][z] + data.keys[j][z] > 7)
                {
                    match = false;
                    break;
                }
            }
            if (match)
                ans++;
        }
    }
    Console.WriteLine(ans);
}


(List<int[]> locks, List<int[]> keys) ReadData(bool test = false)
{
    var locks = new List<int[]>();
    var keys = new List<int[]>();
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var pos = 0;
    while (pos < lines.Length)
    {
        var pattern = new int[lines[0].Length];
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                if (lines[i + pos][j] == '#') pattern[j]++;
            }
        }
        if (lines[pos][0] == '#')
            locks.Add(pattern);
        else keys.Add(pattern);
        pos += 8;
    }
    return (locks, keys);
}
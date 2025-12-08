PartOne(true);
PartOne(false);
PartTwo(true);
PartTwo(false);
void PartOne(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    var limit = 1000;
    if (test)
    {
        Console.Write("Test:");
        limit = 10;
    }
    var pq = new PriorityQueue<(int x, int y), double>();
    for (int i = 0; i < data.Count; i++)
        for (int j = i + 1; j < data.Count; j++)
        {
            var dist = 1.0 * (data[i][0] - data[j][0]) * (data[i][0] - data[j][0]) +
            1.0 * (data[i][1] - data[j][1]) * (data[i][1] - data[j][1]) +
            1.0 * (data[i][2] - data[j][2]) * (data[i][2] - data[j][2]);
            pq.Enqueue((i, j), dist);
        }
    var ds = new int[data.Count];
    for (int i = 0; i < ds.Length; i++)
        ds[i] = i;
    for (int i = 0; i < limit; i++)
    {
        var pair = pq.Dequeue();
        var a = ds[pair.x];
        var b = ds[pair.y];
        for (int j = 0; j < ds.Length; j++)
        {
            if (ds[j] == b)
                ds[j] = a;
        }
    }
    var count = new Dictionary<int, int>();
    foreach (var d in ds)
    {
        count.TryGetValue(d, out var v);
        count[d] = v + 1;
    }
    ans = 1;
    count.Values.OrderByDescending(x => x).Take(3).ToList().ForEach(x => ans *= x);
    Console.WriteLine(ans);
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    var limit = 1000;
    if (test)
    {
        Console.Write("Test:");
        limit = 10;
    }
    var pq = new PriorityQueue<(int x, int y), double>();
    for (int i = 0; i < data.Count; i++)
        for (int j = i + 1; j < data.Count; j++)
        {
            var dist = 1.0 * (data[i][0] - data[j][0]) * (data[i][0] - data[j][0]) +
            1.0 * (data[i][1] - data[j][1]) * (data[i][1] - data[j][1]) +
            1.0 * (data[i][2] - data[j][2]) * (data[i][2] - data[j][2]);
            pq.Enqueue((i, j), dist);
        }
    var ds = new int[data.Count];
    var set = new HashSet<int>();
    for (int i = 0; i < ds.Length; i++)
    {
        ds[i] = i;
        set.Add(i);
    }
    while (true)
    {
        var pair = pq.Dequeue();
        var a = ds[pair.x];
        var b = ds[pair.y];
        if (a == b) continue;
        for (int j = 0; j < ds.Length; j++)
        {
            if (ds[j] == b)
                ds[j] = a;
        }
        set.Remove(b);
        if (set.Count == 1)
        {
            ans = (long)data[pair.x][0] * data[pair.y][0];
            break;
        }
    }

    Console.WriteLine(ans);
}
IList<int[]> ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    return lines.Select(x => x.Split(',').Select(x => int.Parse(x)).ToArray()).ToList();
}
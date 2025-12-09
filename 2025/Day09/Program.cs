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
    for (int i = 0; i < data.Count; i++)
        for (int j = i + 1; j < data.Count; j++)
        {
            ans = Math.Max(ans, (long)(Math.Abs(data[i].x - data[j].x) + 1) * (1 + Math.Abs(data[i].y - data[j].y)));
        }
    Console.WriteLine(ans);
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
    {
        Console.WriteLine("Test:");
    }

    var compX = new Dictionary<int, int>();
    var compY = new Dictionary<int, int>();
    compX[0] = 0;
    compY[0] = 0;

    foreach (var xOrd in data.OrderBy(x => x.x))
    {
        if (!compX.ContainsKey(xOrd.x))
            compX[xOrd.x] = compX.Count;
    }

    foreach (var yOrd in data.OrderBy(x => x.y))
    {
        if (!compY.ContainsKey(yOrd.y))
            compY[yOrd.y] = compY.Count;
    }
    //set limit
    compX[1000000] = compX.Count;
    compY[1000000] = compY.Count;
    var fill = new bool[compX.Count, compY.Count];
    for (int i = 0; i < data.Count; i++)
    {
        if (data[i].x == data[(i + 1) % data.Count].x)
        {
            var min = Math.Min(compY[data[i].y], compY[data[(i + 1) % data.Count].y]);
            var max = Math.Max(compY[data[i].y], compY[data[(i + 1) % data.Count].y]);
            for (int y = min; y <= max; y++)
                fill[compX[data[i].x], y] = true;
        }
        else
        {
            var min = Math.Min(compX[data[i].x], compX[data[(i + 1) % data.Count].x]);
            var max = Math.Max(compX[data[i].x], compX[data[(i + 1) % data.Count].x]);
            for (int x = min; x <= max; x++)
                fill[x, compY[data[i].y]] = true;
        }
    }


    FillInside(fill, compX.Count, compY.Count);

    for (int i = 0; i < data.Count; i++)
        for (int j = i + 1; j < data.Count; j++)
        {
            var minx = Math.Min(data[i].x, data[j].x);
            var maxx = Math.Max(data[i].x, data[j].x);
            var miny = Math.Min(data[i].y, data[j].y);
            var maxy = Math.Max(data[i].y, data[j].y);
            var size = (long)(Math.Abs(data[i].x - data[j].x) + 1) * (1 + Math.Abs(data[i].y - data[j].y));
            if (size > ans && Good(compX[minx], compX[maxx], compY[miny], compY[maxy], fill))
                ans = size;

        }
    Console.WriteLine(ans);
}
bool Good(int minx, int maxx, int miny, int maxy, bool[,] filled)
{
    for (int x = minx; x <= maxx; x++)
        for (int y = miny; y <= maxy; y++)
        {
            if (!filled[x, y])
                return false;
        }
    return true;
}
void FillInside(bool[,] toFill, int n, int m)
{
    var visited = new bool[n, m];
    var directions = new (int dx, int dy)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };
    var queue = new Queue<(int x, int y)>();
    queue.Enqueue((0, 0));
    visited[0, 0] = true;
    while (queue.Count > 0)
    {
        var (x, y) = queue.Dequeue();
        foreach (var (dx, dy) in directions)
        {
            var nx = x + dx;
            var ny = y + dy;
            if (nx >= 0 && nx < n && ny >= 0 && ny < m && !visited[nx, ny] && !toFill[nx, ny])
            {
                visited[nx, ny] = true;
                queue.Enqueue((nx, ny));
            }
        }
    }
    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
        {
            if (!visited[i, j])
                toFill[i, j] = true;
        }
}
IList<(int x, int y)> ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    return lines.Select(line =>
    {
        var parts = line.Split(',');
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }).ToList();
}
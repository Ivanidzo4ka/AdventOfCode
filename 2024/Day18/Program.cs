var dir = new (int x, int y)[] { (0, 1), (1, 0), (0, -1), (-1, 0), };

PartOne(true);
PartOne(false);
PartTwo(true);
PartTwo(false);
void PartOne(bool test = false)
{
    var data = ReadData(test);

    var ans = 0L;
    var first = 1024;
    if (test)
    {
        Console.Write("Test:");
        first = 12;
    }
    var n = data.Max(x => x.x);
    var pq = new PriorityQueue<(int x, int y, int cost), int>();
    var walls = new HashSet<(int x, int y)>();
    for (int i = 0; i < first; i++)
        walls.Add((data[i].x, data[i].y));
    pq.Enqueue((0, 0, 0), 0);
    while (pq.Count != 0)
    {
        var cur = pq.Dequeue();
        if (cur.x == n && cur.y == n)
        {
            Console.WriteLine(cur.cost); break;
        }
        if (!walls.Add((cur.x, cur.y)))
        {
            continue;
        }
        for (int d = 0; d < 4; d++)
        {
            var newx = cur.x + dir[d].x;
            var newy = cur.y + dir[d].y;
            if (newx < 0 || newx == n + 1 || newy < 0 || newy == n + 1) continue;
            if (!walls.Contains((newx, newy)))
            {
                pq.Enqueue((newx, newy, cur.cost + 1), cur.cost + 1);
            }
        }
    }
    Console.WriteLine(ans);
}

void PartTwo(bool test = false)
{
    var data = ReadData(test);

    var first = 1024;
    if (test)
    {
        Console.Write("Test:");
        first = 12;
    }
    var n = data.Max(x => x.x);
    while (true)
    {
        bool can = false;
        var pq = new PriorityQueue<(int x, int y, int cost), int>();
        var walls = new HashSet<(int x, int y)>();

        for (int i = 0; i < first; i++)
            walls.Add((data[i].x, data[i].y));

        pq.Enqueue((0, 0, 0), 0);
        while (pq.Count != 0)
        {
            var cur = pq.Dequeue();
            if (cur.x == n && cur.y == n)
            {
                can = true; break;
            }
            if (!walls.Add((cur.x, cur.y)))
            {
                continue;
            }
            for (int d = 0; d < 4; d++)
            {
                var newx = cur.x + dir[d].x;
                var newy = cur.y + dir[d].y;
                if (newx < 0 || newx == n + 1 || newy < 0 || newy == n + 1) continue;
                if (!walls.Contains((newx, newy)))
                {
                    pq.Enqueue((newx, newy, cur.cost + 1), cur.cost + 1);
                }
            }
        }
        if (!can)
        {
            Console.WriteLine($"{data[first - 1].x},{data[first - 1].y}");
            break;
        }
        first++;
    }
}

List<(int x, int y)> ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var result = new List<(int x, int y)>();
    foreach (var line in lines)
    {
        var t = line.Split(",");
        result.Add((int.Parse(t[0]), int.Parse(t[1])));
    }
    return result;
}
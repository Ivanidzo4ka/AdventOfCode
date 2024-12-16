
var dir = new (int x, int y)[] { (0, 1), (1, 0), (0, -1), (-1, 0), };

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
    var n = data.Length;
    var m = data[0].Length;
    var startx = 0;
    var starty = 0;
    var endx = 0;
    var endy = 0;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
        {
            if (data[i][j] == 'S')
            {
                startx = i;
                starty = j;
            }
            else if (data[i][j] == 'E')
            {
                endx = i;
                endy = j;
            }
        }
    var pq = new PriorityQueue<(int x, int y, int d, int cost), int>();
    pq.Enqueue((startx, starty, 0, 0), 0);
    var visited = new HashSet<(int x, int y, int d)>();

    while (pq.Count != 0)
    {
        var cur = pq.Dequeue();
        if (cur.x == endx && cur.y == endy)
        {
            ans = cur.cost;
            break;
        }
        visited.Add((cur.x, cur.y, cur.d));
        var nextx = cur.x + dir[cur.d].x;
        var nexty = cur.y + dir[cur.d].y;
        if (data[nextx][nexty] != '#' && !visited.Contains((nextx, nexty, cur.d)))
        {
            pq.Enqueue((nextx, nexty, cur.d, cur.cost + 1), cur.cost + 1);
        }
        if (!visited.Contains((cur.x, cur.y, (cur.d + 1) % 4)))
            pq.Enqueue((cur.x, cur.y, (cur.d + 1) % 4, cur.cost + 1000), cur.cost + 1000);
        if (!visited.Contains((cur.x, cur.y, (cur.d + 3) % 4)))
            pq.Enqueue((cur.x, cur.y, (cur.d + 3) % 4, cur.cost + 1000), cur.cost + 1000);

    }

    Console.WriteLine(ans);
}

void PartTwo(bool test = false)
{
    var data = ReadData(test);
    if (test)
        Console.Write("Test:");
    var n = data.Length;
    var m = data[0].Length;
    var startx = 0;
    var starty = 0;
    var endx = 0;
    var endy = 0;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
        {
            if (data[i][j] == 'S')
            {
                startx = i;
                starty = j;
            }
            else if (data[i][j] == 'E')
            {
                endx = i;
                endy = j;
            }
        }
    var pq = new PriorityQueue<(int x, int y, int d, int cost), int>();
    pq.Enqueue((startx, starty, 0, 0), 0);
    var visited = new Dictionary<(int x, int y, int d), int>();

    while (pq.Count != 0)
    {
        var cur = pq.Dequeue();
        if (visited.TryGetValue((cur.x, cur.y, cur.d), out var c))
            continue;
        visited[(cur.x, cur.y, cur.d)] = cur.cost;
        var nextx = cur.x + dir[cur.d].x;
        var nexty = cur.y + dir[cur.d].y;
        if (data[nextx][nexty] != '#' && !visited.ContainsKey((nextx, nexty, cur.d)))
        {
            pq.Enqueue((nextx, nexty, cur.d, cur.cost + 1), cur.cost + 1);
        }
        if (!visited.ContainsKey((cur.x, cur.y, (cur.d + 1) % 4)))
            pq.Enqueue((cur.x, cur.y, (cur.d + 1) % 4, cur.cost + 1000), cur.cost + 1000);
        if (!visited.ContainsKey((cur.x, cur.y, (cur.d + 3) % 4)))
            pq.Enqueue((cur.x, cur.y, (cur.d + 3) % 4, cur.cost + 1000), cur.cost + 1000);

    }
    var min = int.MaxValue;
    for (int i = 0; i < 4; i++)
    {
        min = Math.Min(min, visited[(endx, endy, i)]);
    }
    var queue = new Queue<(int x, int y, int d, int cost)>();
    for (int i = 0; i < 4; i++)
    {
        if (visited[(endx, endy, i)] == min) queue.Enqueue((endx, endy, i, min));
    }
    var set = new HashSet<(int x, int y)>();
    while (queue.Count != 0)
    {

        var cur = queue.Dequeue();
        set.Add((cur.x, cur.y));
        var newx = cur.x - dir[cur.d].x;
        var newy = cur.y - dir[cur.d].y;
        if (visited.TryGetValue((newx, newy, cur.d), out var t) && t == cur.cost - 1)
        {
            queue.Enqueue((newx, newy, cur.d, cur.cost - 1));
        }
        var d = (cur.d + 3) % 4;

        if (visited.TryGetValue((cur.x, cur.y, d), out t) && t == cur.cost - 1000)
        {
            queue.Enqueue((cur.x, cur.y, d, cur.cost - 1000));
        }
        d = (cur.d + 1) % 4;
        if (visited.TryGetValue((cur.x, cur.y, d), out t) && t == cur.cost - 1000)
        {
            queue.Enqueue((cur.x, cur.y, d, cur.cost - 1000));
        }
    }
    Console.WriteLine(set.Count);

}


char[][] ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    return lines.Select(x => x.ToCharArray()).ToArray();
}
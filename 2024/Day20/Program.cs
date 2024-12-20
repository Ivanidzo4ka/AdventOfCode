

var dist = new Dictionary<(int x, int y), int>();
var dir = new (int x, int y)[] { (0, 1), (1, 0), (0, -1), (-1, 0), };

// PartOne(true);
// PartOne(false);
PartTwo(true);
PartTwo(false);

void PartOne(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    var startx = 0;
    var starty = 0;
    var endx = 0;
    var endy = 0;
    for (int i = 0; i < data.Length; i++)
        for (int j = 0; j < data.Length; j++)
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
    var cur = GetPath(data, startx, starty, endx, endy);
    var diffs = new Dictionary<int, int>();
    for (int pos = 0; pos < cur.Count - 1; pos++)
        for (int i = 0; i < 4; i++)
        {
            var newx = cur[pos].x + dir[i].x;
            var newy = cur[pos].y + dir[i].y;
            if (newx < 0 || newy < 0 || newx == data.Length || newy == data.Length) continue;
            if (data[newx][newy] == '#')
            {
                for (int z = 0; z < 4; z++)
                {
                    var nextx = newx + dir[z].x;
                    var nexty = newy + dir[z].y;
                    if (nextx < 0 || nexty < 0 || nextx == data.Length || nexty == data.Length
                    || (nextx == cur[pos].x && nexty == cur[pos].y)
                    || data[nextx][nexty] == '#') continue;
                    var p = GetPath(data, nextx, nexty, endx, endy);
                    var dif = cur.Count - pos - p.Count - 2;
                    if (dif > 0)
                    {
                        diffs.TryGetValue(dif, out var c);
                        diffs[dif] = c + 1;
                    }

                }
            }
        }

    foreach (var kvp in diffs)
    {
        if (kvp.Key >= 100)
            ans += kvp.Value;
    }
    Console.WriteLine(ans);
}
List<(int x, int y)> GetPath(char[][] board, int startx, int starty, int endx, int endy)
{
    var queue = new PriorityQueue<(int x, int y, int step, int dir), int>();
    queue.Enqueue((startx, starty, 0, -1), 0);
    var visited = new Dictionary<(int x, int y), int>();

    while (queue.Count != 0)
    {
        var cur = queue.Dequeue();
        if (!visited.TryAdd((cur.x, cur.y), cur.dir)) continue;
        if (cur.x == endx && cur.y == endy) break;

        for (int i = 0; i < 4; i++)
        {
            var newx = cur.x + dir[i].x;
            var newy = cur.y + dir[i].y;
            if (newx < 0 || newy < 0 || newx == board.Length || newy == board.Length) continue;
            if (board[newx][newy] != '#')
            {
                queue.Enqueue((newx, newy, cur.step + 1, i), cur.step + 1);
            }
        }
    }
    var curd = visited[(endx, endy)];
    var ans = new List<(int x, int y)>();
    while (curd != -1)
    {
        ans.Add((endx, endy));
        endx -= dir[curd].x;
        endy -= dir[curd].y;
        curd = visited[(endx, endy)];
    }
    ans.Add((startx, starty));
    ans.Reverse();
    return ans;
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");

    var diffs = new Dictionary<int, int>();
    var startx = 0;
    var starty = 0;
    var endx = 0;
    var endy = 0;
    for (int i = 0; i < data.Length; i++)
        for (int j = 0; j < data.Length; j++)
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
    var maxJump = 20;
    var cur = GetPath(data, startx, starty, endx, endy);
    for (int pos = 0; pos < cur.Count; pos++)
        dist[(cur[pos].x, cur[pos].y)] = cur.Count - 1 - pos;
    for (int pos = 0; pos < cur.Count - 1; pos++)
    {
        for (int i = 0; i < data.Length; i++)
            for (int j = 0; j < data.Length; j++)
            {
                var jump = Math.Abs(cur[pos].x - i) + Math.Abs(cur[pos].y - j);
                if (data[i][j] != '#' && jump <= maxJump)
                {
                    var dist = GetDist(data, i, j);
                    var dif = cur.Count - pos - dist - jump - 1;
                    if (dif > 0)
                    {
                        diffs.TryGetValue(dif, out var c);
                        diffs[dif] = c + 1;
                    }
                }
            }
    }
    foreach (var kvp in diffs)
    {
        if (kvp.Key >= 100)
            ans += kvp.Value;
    }
    Console.WriteLine(ans);
}

int GetDist(char[][] board, int startx, int starty)
{
    if (dist.TryGetValue((startx, starty), out var ans))
        return ans;

    var queue = new PriorityQueue<(int x, int y, int step), int>();
    queue.Enqueue((startx, starty, 0), 0);
    var visited = new HashSet<(int x, int y)>();

    while (queue.Count != 0)
    {
        var cur = queue.Dequeue();
        if (!visited.Add((cur.x, cur.y))) continue;
        if (dist.TryGetValue((cur.x, cur.y), out var found))
        {
            dist[(startx, starty)] = cur.step + found;
            break;
        }


        for (int i = 0; i < 4; i++)
        {
            var newx = cur.x + dir[i].x;
            var newy = cur.y + dir[i].y;
            if (newx < 0 || newy < 0 || newx == board.Length || newy == board.Length) continue;
            if (board[newx][newy] != '#')
            {
                queue.Enqueue((newx, newy, cur.step + 1), cur.step + 1);
            }
        }
    }
    if (dist.TryGetValue((startx, starty), out ans))
        return ans;
    return int.MaxValue / 2;
}

char[][] ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");

    return lines.Select(x => x.ToCharArray()).ToArray();
}
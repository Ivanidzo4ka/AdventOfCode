
var dir = new (int x, int y)[] { (0, 1), (1, 0), (-1, 0), (0, -1) };

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

    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            if (data[i][j] != ' ')
            {
                ans += Walk(data, i, j);
            }
        }
    Console.WriteLine(ans);
}
int Walk(char[][] field, int x, int y)
{
    var n = field.Length;
    var queue = new Queue<(int x, int y)>();
    bool[,] visited = new bool[n, n];
    var l = field[x][y];
    field[x][y] = ' ';
    visited[x, y] = true;
    queue.Enqueue((x, y));
    var count = 0;
    var walls = 0;
    while (queue.Count != 0)
    {
        var cur = queue.Dequeue();
        count++;
        for (int d = 0; d < 4; d++)
        {
            var newx = cur.x + dir[d].x;
            var newy = cur.y + dir[d].y;
            if (newx < 0 || newy < 0 || newx == n || newy == n) { walls++; continue; }
            if (field[newx][newy] == l)
            {
                queue.Enqueue((newx, newy));
                field[newx][newy] = ' ';
                visited[newx, newy] = true;
            }
            else if (!visited[newx, newy])
                walls++;
        }
    }
    return count * walls;

}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    var n = data.Length;

    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            if (data[i][j] != ' ')
            {
                ans += Walk2(data, i, j);
            }
        }
    Console.WriteLine(ans);
}

int Walk2(char[][] field, int x, int y)
{
    var n = field.Length;
    var queue = new Queue<(int x, int y)>();
    bool[,] visited = new bool[n, n];
    var l = field[x][y];
    field[x][y] = ' ';
    visited[x, y] = true;
    queue.Enqueue((x, y));
    var count = 0;
    var walls = 0;
    while (queue.Count != 0)
    {
        var cur = queue.Dequeue();
        count++;
        for (int d = 0; d < 4; d++)
        {
            var newx = cur.x + dir[d].x;
            var newy = cur.y + dir[d].y;
            if (newx < 0 || newy < 0 || newx == n || newy == n) { continue; }
            if (field[newx][newy] == l)
            {
                queue.Enqueue((newx, newy));
                field[newx][newy] = ' ';
                visited[newx, newy] = true;
            }
        }
    }
    bool? prev = null;
    var scan = new bool[n];
    for (int i = 0; i < n; i++)
    {
        prev = null;
        for (int j = 0; j < n; j++)
        {
            if (visited[i, j] != scan[j])
            {
                if (prev != visited[i, j])
                    walls++;

                scan[j] = visited[i, j];
                prev = scan[j];
            }
            else prev = null;
        }
    }
    for (int i = 0; i < n; i++)
        if (scan[i])
        {
            if (prev == null) { walls++; scan[i] = false; prev = true; }
        }
        else prev = null;
    Array.Fill(scan, false);

    for (int i = 0; i < n; i++)
    {
        prev = null;
        for (int j = 0; j < n; j++)
        {
            if (visited[j, i] != scan[j])
            {
                if (prev != visited[j, i])
                    walls++;

                scan[j] = visited[j, i];
                prev = scan[j];
            }
            else prev = null;
        }
    }
    for (int i = 0; i < n; i++)
        if (scan[i])
        {
            if (prev == null) { walls++; scan[i] = false; prev = true; }
        }
        else prev = null;
    Array.Fill(scan, false);

    return count * walls;

}

char[][] ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    return lines.Select(x => x.ToCharArray()).ToArray();
}
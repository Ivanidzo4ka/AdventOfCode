using System.Text;
//                                   R      D        L         U
var dir = new (int x, int y)[5] { (0, 1), (1, 0), (0, -1), (-1, 0), (0, 0) };
StepOne();
StepTwo();
void StepOne()
{
    var l = new List<(int x, int y, int d)>();

    var index = 0;
    var len = 0;
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        len = line.Length;
        for (int i = 0; i < line.Length; i++)
        {

            if (line[i] == '>')
            {
                l.Add((index, i, 0));
            }
            else if (line[i] == '^')
            {
                l.Add((index, i, 3));
            }
            else if (line[i] == '<')
            {
                l.Add((index, i, 2));
            }
            else if (line[i] == 'v')
                l.Add((index, i, 1));

        }
        index++;
    }

    var n = index;
    var m = len;
    var ans = Walk(n, m, 0, 1,n-1,m-2, ref l);
    Console.WriteLine(ans);
}
void StepTwo()
{
    var l = new List<(int x, int y, int d)>();

    var index = 0;
    var len = 0;
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        len = line.Length;
        for (int i = 0; i < line.Length; i++)
        {

            if (line[i] == '>')
            {
                l.Add((index, i, 0));
            }
            else if (line[i] == '^')
            {
                l.Add((index, i, 3));
            }
            else if (line[i] == '<')
            {
                l.Add((index, i, 2));
            }
            else if (line[i] == 'v')
                l.Add((index, i, 1));

        }
        index++;
    }

    var n = index;
    var m = len;
    var ans = Walk(n, m, 0, 1, n - 1, m - 2, ref l);
    ans += Walk(n, m, n - 1, m - 2, 0, 1, ref l);
    ans += Walk(n, m, 0, 1, n - 1, m - 2, ref l);
    Console.WriteLine(ans);
}
int Walk(int n, int m, int startx, int starty, int endx, int endy, ref List<(int x, int y, int d)> l)
{
    var minutes = 0;
    var queue = new Queue<(int x, int y)>();
    queue.Enqueue((startx, starty));
    var work = true;
    while (work)
    {
        minutes++;
        var set = Move(ref l, n, m);
        var next = new Queue<(int x, int y)>();
        while (queue.Count != 0)
        {
            var cur = queue.Dequeue();
            for (int d = 0; d < 5; d++)
            {
                var newx = cur.x + dir[d].x;
                var newy = cur.y + dir[d].y;
                if (newx < 0 || newy < 0 || newx == n || newy == m)
                    continue;
                if (newx == 0 || newx == n - 1 || newy == 0 || newy == m - 1)
                {
                    if (!((newx == 0 && newy == 1) || (newx == n - 1 && newy == m - 2)))
                        continue;
                }
                if (!set.Contains((newx, newy)))
                {
                    if (newx == endx && newy == endy)
                    {
                        work = false;
                    }
                    next.Enqueue((newx, newy));
                    set.Add((newx, newy));
                }
            }
        }
        queue = next;
    }
    return minutes;
}
HashSet<(int x, int y)> Move(ref List<(int x, int y, int d)> bliz, int n, int m)
{
    var set = new HashSet<(int x, int y)>();
    var newL = new List<(int x, int y, int d)>();
    foreach (var b in bliz)
    {
        var newx = b.x + dir[b.d].x;
        var newy = b.y + dir[b.d].y;
        if (newx == 0 || newx == n - 1)
            if (newx == 0)
                newx = n - 2;
            else
                newx = 1;

        if (newy == 0 || newy == m - 1)
            if (newy == 0)
                newy = m - 2;
            else
                newy = 1;
        newL.Add((newx, newy, b.d));
        set.Add((newx, newy));
    }
    bliz = newL;
    // for (int i = 0; i < n; i++)
    // {
    //     var sb = new StringBuilder();
    //     for (int j = 0; j < m; j++)
    //     {

    //         if (set.Contains((i, j))) sb.Append("#"); else sb.Append(".");
    //     }
    //     Console.WriteLine(sb.ToString());
    // }
    // Console.WriteLine();
    return set;
}

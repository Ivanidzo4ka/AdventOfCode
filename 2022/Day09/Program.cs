
var dir = new (int x, int y)[8] { (0, -1), (0, 1), (1, 0), (-1, 0), (-1, -1), (1, 1), (-1, 1), (1, -1) };
StepOne();
StepTwo();

void StepOne()
{
    var head = new Point(0, 0);
    var tail = new Point(0, 0);
    var visited = new HashSet<Point>();
    visited.Add(tail);
    var dMap = new Dictionary<string, int>() { ["R"] = 1, ["L"] = 0, ["U"] = 2, ["D"] = 3 };

    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var split = line.Split(" ");
        var d = dMap[split[0]];
        var steps = int.Parse(split[1]);
        while (steps > 0)
        {
            head = new Point(head.X + dir[d].x, head.Y + dir[d].y);
            steps--;
            tail = MoveTail(head, tail);
            visited.Add(tail);
        }

    }
    Console.WriteLine(visited.Count);
}


void StepTwo()
{
    var head = new Point(0, 0);
    var N = 9;
    var tails = new Point[N];
    for (int i = 0; i < N; i++)
        tails[i] = new Point(0, 0);
    var visited = new HashSet<Point>();
    visited.Add(head);
    var dMap = new Dictionary<string, int>() { ["R"] = 1, ["L"] = 0, ["U"] = 2, ["D"] = 3 };

    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var split = line.Split(" ");
        var d = dMap[split[0]];
        var steps = int.Parse(split[1]);
        while (steps > 0)
        {
            head = new Point(head.X + dir[d].x, head.Y + dir[d].y);
            steps--;
            tails[0] = MoveTail(head, tails[0]);
            for (int i = 1; i < N; i++)
                tails[i] = MoveTail(tails[i - 1], tails[i]);
            visited.Add(tails[8]);
            //Visualize(head, tails);
        }

    }
    Console.WriteLine(visited.Count);
}
Point MoveTail(Point head, Point tail)
{
    if (Math.Abs(head.X - tail.X) < 2 && Math.Abs(head.Y - tail.Y) < 2)
        return new Point(tail.X, tail.Y);
    var start = 0;
    // if we on same row/column we will move with first 4 steps, if not, move by diag which starts from 4;
    if (head.X - tail.X != 0 && head.Y - tail.Y != 0)
        start = 4;
    for (int i = start; i < 8; i++)
        if (Math.Abs(head.X - (tail.X + dir[i].x)) < 2 && Math.Abs(head.Y - (tail.Y + dir[i].y)) < 2)
            return new Point(tail.X + dir[i].x, tail.Y + dir[i].y);
    return null;

}

void Visualize(Point head, Point[] tails)
{

    int N = 50;
    var arr = new char[N, N];

    for (int i = 8; i >= 0; i--)
    {
        arr[tails[i].X + N / 2, tails[i].Y + N / 2] = (char)(i + '0');
    }
    arr[head.X + N / 2, head.Y + N / 2] = 'H';
    for (int i = 0; i < N; i++)
    {
        for (int j = 0; j < N; j++)
            Console.Write(arr[i, j]);
        Console.WriteLine();
    }

}
record Point(int X, int Y);
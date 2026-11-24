
PartOne();
PartTwo();
void PartOne()
{
    var (x, y, number) = Parse("input.txt");
    var startx =1;
    var starty =1;
    var visited = new Dictionary<(int x, int y),int>();
    var priorityQueue = new PriorityQueue<(int x, int y), int>();
    priorityQueue.Enqueue((startx, starty), Dist(startx, starty, x, y));
    visited.Add((startx, starty), 0);
    var directions = new (int dx, int dy)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };
    while (priorityQueue.Count>0)
    {
        var (currentX, currentY) = priorityQueue.Dequeue();
        if (currentX == x && currentY == y)
        {
            Console.WriteLine($"Found target at {currentX},{currentY} with distance {visited[(currentX, currentY)]}");
            break;
        }
        for(int i=0;i<directions.Length;i++)
        {
            var (dx, dy) = directions[i];
            var newX = currentX + dx;
            var newY = currentY + dy;
            if (newX < 0 || newY < 0)
                continue;
            if (IsWall(newX, newY,number))
                continue;
            if (visited.ContainsKey((newX, newY)))
                continue;
            visited.Add((newX, newY), visited[(currentX, currentY)] + 1);
            priorityQueue.Enqueue((newX, newY), Dist(newX, newY, x, y));
        }
    }
}

void PartTwo()
{
    var (x, y, number) = Parse("input.txt");
    var startx =1;
    var starty =1;
    var visited = new HashSet<(int x, int y)>();
    var queue = new Queue<(int x, int y)>();
    var next = new Queue<(int x, int y)>();
    visited.Add((startx, starty));
    queue.Enqueue((startx, starty));
    var step=0;
    var directions = new (int dx, int dy)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };
    while (step<50)
    {
        while (queue.Count > 0)
        {
            var (currentX, currentY) = queue.Dequeue(); 
            for(int i=0;i<directions.Length;i++)
            {
                var (dx, dy) = directions[i];
                var newX = currentX + dx;
                var newY = currentY + dy;
                if (newX < 0 || newY < 0)
                    continue;
                if (IsWall(newX, newY,number))
                    continue;
                if (visited.Contains((newX, newY)))
                    continue;
                visited.Add((newX, newY));
                next.Enqueue((newX, newY));
            }
        }
        (queue, next)=(next,queue);
        step++;
    }
    Console.WriteLine($"Number of locations reachable in 50 steps: {visited.Count}");
}
int Dist(int x1, int y1, int x2, int y2)
{
    return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
}
 bool IsWall(long x, long y, int number)
{
    var t = x*x + 3*x +2*x*y +y +y*y;
    t+= number;
    return BitCount(t)%2==1;;
}

int BitCount(long n)
{
    var c =0;
    while (n!=0)
    {
        c+= (int)(n&1);
        n>>=1;
    }
    return c;
}
(int x, int y, int number)Parse(string input)
{
    var lines=File.ReadAllLines(input);
    var x = int.Parse(lines[0].Split(',')[0]);
    var y = int.Parse(lines[0].Split(',')[1]);
    var number = int.Parse(lines[1]);

    return (x,y,number);
}
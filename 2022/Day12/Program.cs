using System.Text;
//StepOne();
StepTwo();
void StepOne()
{
    var map = new List<List<char>>();

    foreach (var line in File.ReadAllLines("input.txt"))
        map.Add(line.Select(x => x).ToList());
    var startx = 0;
    var starty = 0;
    var endx = 0;
    var endy = 0;
    for (int i = 0; i < map.Count; i++)
    {
        for (int j = 0; j < map[0].Count; j++)
        {
            if (map[i][j] == 'S')
            {
                startx = i;
                starty = j;
                map[i][j] = 'a';
            }
            if (map[i][j] == 'E')
            {
                map[i][j] = 'z';
                endx = i;
                endy = j;
            }
        }
    }
    Console.WriteLine(Search(map, startx, starty, endx, endy));
}

int Search(List<List<char>> map, int startx, int starty, int endx, int endy)
{
    var visited = new bool[map.Count][];
    for (int i = 0; i < map.Count; i++)
    {
        visited[i] = new bool[map[0].Count];
    }

    var dir = new (int x, int y)[4] { (0, -1), (0, 1), (1, 0), (-1, 0) };
    Queue<(int x, int y)> queue = new Queue<(int x, int y)>();
    Queue<(int x, int y)> next = new Queue<(int x, int y)>();
    queue.Enqueue((startx, starty));
    visited[startx][starty] = true;
    var step = 0;
    while (queue.Count != 0)
    {
        step++;
        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();
            for (int i = 0; i < 4; i++)
            {
                var nx = x + dir[i].x;
                var ny = y + dir[i].y;
                if (nx < 0 || ny < 0 || nx == map.Count || ny == map[0].Count)
                    continue;
                if (map[nx][ny] - map[x][y] <= 1 && !visited[nx][ny])
                {
                    if (nx == endx && ny == endy)
                    {
                        return step;
                    }
                    visited[nx][ny] = true;
                    next.Enqueue((nx, ny));
                }
            }
        }
        queue = next;
        next = new Queue<(int x, int y)>();
    }
    return int.MaxValue;
}
void StepTwo()
{
    var map = new List<List<char>>();

    foreach (var line in File.ReadAllLines("input.txt"))
        map.Add(line.Select(x => x).ToList());


    var endx = 0;
    var endy = 0;
    for (int i = 0; i < map.Count; i++)
    {
        for (int j = 0; j < map[0].Count; j++)
        {
            if (map[i][j] == 'S')
            {
                map[i][j] = 'a';

            }
            if (map[i][j] == 'E')
            {
                map[i][j] = 'z';
                endx = i;
                endy = j;
            }
        }
    }
    var min = int.MaxValue;
    for (int i = 0; i < map.Count; i++)
    {
        for (int j = 0; j < map[0].Count; j++)
        {
            if (map[i][j] == 'a')
            {
                var mapCopy = new List<List<char>>();
                foreach (var l in map)
                {
                    mapCopy.Add(l.Select(x => x).ToList());
                }
                min = Math.Min(min, Search(mapCopy, i, j, endx, endy));
            }
        }
    }
    Console.WriteLine(min);
}
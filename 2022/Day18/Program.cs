var sides = new (int x, int y, int z)[] { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1) };
var traps = new HashSet<(int x, int y, int z)>();
var notTraps = new HashSet<(int x, int y, int z)>();
int minx = int.MaxValue;
int miny = int.MaxValue;
int minz = int.MaxValue;
int maxx = int.MaxValue;
int maxy = int.MaxValue;
int maxz = int.MaxValue;
StepOne();
StepTwo();

void StepOne()
{
    var dim = new int[30, 30, 30];
    var cubes = new HashSet<(int x, int y, int z)>();
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var split = line.Split(",");
        var x = int.Parse(split[0]);
        var y = int.Parse(split[1]);
        var z = int.Parse(split[2]);
        cubes.Add((x, y, z));
    }
    var ans = 0;
    foreach (var cube in cubes)
    {
        for (int i = 0; i < sides.Length; i++)
        {
            var newx = cube.x + sides[i].x;
            var newy = cube.y + sides[i].y;
            var newz = cube.z + sides[i].z;
            if (!cubes.Contains((newx, newy, newz)))
                ans++;
        }
    }

    Console.WriteLine(ans);


}

void StepTwo()
{
    var dim = new int[30, 30, 30];
    var cubes = new HashSet<(int x, int y, int z)>();
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var split = line.Split(",");
        var x = int.Parse(split[0]);
        var y = int.Parse(split[1]);
        var z = int.Parse(split[2]);
        cubes.Add((x, y, z));

        maxx = Math.Max(maxx, x);
        maxy = Math.Max(maxy, y);
        maxz = Math.Max(maxz, z);
        minx = Math.Min(minx, x);
        miny = Math.Min(miny, y);
        minz = Math.Min(minz, z);


    }

    var ans = 0;
    foreach (var cube in cubes)
    {
        for (int i = 0; i < sides.Length; i++)
        {
            var newx = cube.x + sides[i].x;
            var newy = cube.y + sides[i].y;
            var newz = cube.z + sides[i].z;
            if (!cubes.Contains((newx, newy, newz)))
                if (!cubes.Contains((newx, newy, newz)))
                    if (CanEscape(newx, newy, newz, cubes))
                        ans++;
        }
    }
    Console.WriteLine(ans);
}



bool CanEscape(int x, int y, int z, HashSet<(int x, int y, int z)> cubes)
{
    var queue = new Queue<(int x, int y, int z)>();
    var visited = new HashSet<(int x, int y, int z)>();
    if (traps.Contains((x, y, z)))
        return false;
    if (notTraps.Contains((x, y, z)))
        return true;
    queue.Enqueue((x, y, z));
    visited.Add((x, y, z));
    while (queue.Count != 0)
    {
        var (cx, cy, cz) = queue.Dequeue();

        for (int i = 0; i < sides.Length; i++)
        {
            var newx = cx + sides[i].x;
            var newy = cy + sides[i].y;
            var newz = cz + sides[i].z;
            if (newx < minx || newy < miny || newz < minz || newx == maxx + 1 || newy == maxy + 1 || newz == maxz + 1)
            {
                foreach (var v in visited)
                {
                    notTraps.Add(v);
                }
                return true;
            }
            if (!cubes.Contains((newx, newy, newz)) && !visited.Contains((newx, newy, newz)))
            {
                queue.Enqueue((newx, newy, newz));
                visited.Add((newx, newy, newz));
            }
        }
    }
    foreach (var v in visited)
    {
        traps.Add(v);
    }
    return false;
}

PartOne();
PartTwo();

void PartOne()
{
    var ans = 0;
    var dir = new Dictionary<char, (int x, int y)>()
    {
        ['^'] = (-1, 0),
        ['v'] = (1, 0),
        ['<'] = (0, -1),
        ['>'] = (0, 1)
    };
    var lines = File.ReadAllLines("input.txt");
    var set = new HashSet<(int x, int y)>();
    var curx = 0;
    var cury = 0;
    set.Add((0, 0));
    foreach (var c in lines[0])
    {
        curx += dir[c].x;
        cury += dir[c].y;
        set.Add((curx, cury));
    }

    Console.WriteLine(set.Count);
}

void PartTwo()
{
    var ans = 0;
    var dir = new Dictionary<char, (int x, int y)>()
    {
        ['^'] = (-1, 0),
        ['v'] = (1, 0),
        ['<'] = (0, -1),
        ['>'] = (0, 1)
    };
    var lines = File.ReadAllLines("input.txt");
    var set = new HashSet<(int x, int y)>();
    var scurx = 0;
    var scury = 0;
    var rcurx = 0;
    var rcury = 0;
    set.Add((0, 0));
    var s = true;
    foreach (var c in lines[0])
    {
        if (s)
        {
            scurx += dir[c].x;
            scury += dir[c].y;
            set.Add((scurx, scury));
         
        }
        else
        {
            rcurx += dir[c].x;
            rcury += dir[c].y;
            set.Add((rcurx, rcury));
        }
        s=!s;
    }

    Console.WriteLine(set.Count);
}

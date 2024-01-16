
PartOne();
PartTwo();

void PartOne()
{
    var ans = 0L;
    var data = Parse("input.txt");
    Run(ref data, 100, false);
    ans = Calc(data);
    Console.WriteLine(ans);
}
void Run(ref char[][] data, int steps, bool corners)
{
    if (corners)
    {
        data[0][0] = '#';
        data[0][data.Length - 1] = '#';
        data[data.Length - 1][0] = '#';
        data[data.Length - 1][data.Length - 1] = '#';
    }
    var next = new char[data.Length][];
    for (int i = 0; i < data.Length; i++)
        next[i] = new char[data.Length];
    for (int step = 1; step <= steps; step++)
    {
        for (int x = 0; x < data.Length; x++)
            for (int y = 0; y < data.Length; y++)
            {
                var c = Count(data, x, y);
                if (data[x][y] == '#')
                    next[x][y] = c is 2 or 3 ? '#' : '.';
                else
                    next[x][y] = c is 3 ? '#' : '.';
            }
        (data, next) = (next, data);
        if (corners)
        {
            data[0][0] = '#';
            data[0][data.Length - 1] = '#';
            data[data.Length - 1][0] = '#';
            data[data.Length - 1][data.Length - 1] = '#';
        }
    }
}
int Calc(char[][] data)
{
    var c = 0;
    for (int x = 0; x < data.Length; x++)
        for (int y = 0; y < data.Length; y++)
            c += data[x][y] == '#' ? 1 : 0;
    return c;
}
int Count(char[][] data, int x, int y)
{
    var c = 0;
    for (int newx = x - 1; newx <= x + 1; newx++)
        for (int newy = y - 1; newy <= y + 1; newy++)
        {
            if (newx < 0 || newy < 0 || newx == data.Length || newy == data.Length || (newx == x && newy == y))
                continue;
            c += data[newx][newy] == '#' ? 1 : 0;
        }
    return c;

}
void PartTwo()
{
    var ans = 0L;
    var data = Parse("input.txt");
    Run(ref data, 100, true);
    ans = Calc(data);
    Console.WriteLine(ans);
}

char[][] Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    var res = new char[lines.Length][];
    for (int i = 0; i < lines.Length; i++)
    {
        res[i] = lines[i].ToArray();
    }
    return res;
}

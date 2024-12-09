using System.Data;
using System.Text;


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
    var map = new Dictionary<char, List<(int x, int y)>>();
    var n = data.Length;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            if (data[i][j] != '.')
            {
                if (!map.TryGetValue(data[i][j], out var l))
                {
                    l = new List<(int x, int y)>();
                    map[data[i][j]] = l;
                }
                l.Add((i, j));
            }
        }
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            bool found = false;
            foreach (var kvp in map)
            {
                if (found) break;
                for (int x = 0; x < kvp.Value.Count; x++)
                    for (int y = x + 1; y < kvp.Value.Count; y++)
                    {
                        var fd = Dist(i, j, kvp.Value[x].x, kvp.Value[x].y);
                        var sd = Dist(i, j, kvp.Value[y].x, kvp.Value[y].y);
                        if (fd > sd) (fd, sd) = (sd, fd);
                        if (fd * 4 == sd && Area(i, j, kvp.Value[x].x, kvp.Value[x].y, kvp.Value[y].x, kvp.Value[y].y) == 0) { found = true; x = kvp.Value.Count; break; }
                    }
            }
            if (found) { ans++; data[i][j] = '$'; }
        }

    Console.WriteLine(ans);

}
void Print(char[][] data)
{
    int n = data.Length;
    for (int i = 0; i < n; i++)
    {
        var sb = new StringBuilder();

        for (int j = 0; j < n; j++)
        {
            sb.Append(data[i][j]);
        }
        Console.WriteLine(sb.ToString());
    }
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
var map = new Dictionary<char, List<(int x, int y)>>();
    var n = data.Length;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            if (data[i][j] != '.')
            {
                if (!map.TryGetValue(data[i][j], out var l))
                {
                    l = new List<(int x, int y)>();
                    map[data[i][j]] = l;
                }
                l.Add((i, j));
            }
        }
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            bool found = false;
            foreach (var kvp in map)
            {
                if (found) break;
                for (int x = 0; x < kvp.Value.Count; x++)
                    for (int y = x + 1; y < kvp.Value.Count; y++)
                    {
                        if (Area(i, j, kvp.Value[x].x, kvp.Value[x].y, kvp.Value[y].x, kvp.Value[y].y) == 0) { found = true; x = kvp.Value.Count; break; }
                    }
            }
            if (found) { ans++; data[i][j] = '$'; }
        }

    Console.WriteLine(ans);
}
int Area(int Ax, int Ay, int Bx, int By, int Cx, int Cy)
{ return Ax * (By - Cy) + Bx * (Cy - Ay) + Cx * (Ay - By); }
int Dist(int x, int y, int i, int j)
{
    return (i - x) * (i - x) + (j - y) * (j - y);
}

char[][] ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    return lines.Select(x => x.ToCharArray()).ToArray();
}
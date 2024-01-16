StepOne();
StepTwo();
void StepOne()
{

    var forest = new List<List<int>>();
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var column = new List<int>(line.Length);
        foreach (var l in line)
        {
            column.Add(l - '0');
        }
        forest.Add(column);
    }
    var counted = new bool[forest.Count, forest[0].Count];
    var ans = 0;
    for (int i = 0; i < forest.Count; i++)
    {
        var min = -1;
        for (int j = 0; j < forest[i].Count; j++)
            if (min < forest[i][j]) { if (!counted[i, j]) { ans++; counted[i, j] = true; } min = forest[i][j]; }

        min = -1;
        for (int j = forest[i].Count - 1; j >= 0; j--)
            if (min < forest[i][j]) { if (!counted[i, j]) { ans++; counted[i, j] = true; } min = forest[i][j]; }
    }

    for (int i = 0; i < forest[0].Count; i++)
    {
        var min = -1;
        for (int j = 0; j < forest.Count; j++)
            if (min < forest[j][i]) { if (!counted[j, i]) { ans++; counted[j, i] = true; } min = forest[j][i]; }

        min = -1;
        for (int j = forest.Count - 1; j >= 0; j--)
            if (min < forest[j][i]) { if (!counted[j, i]) { ans++; counted[j, i] = true; } min = forest[j][i]; }
    }

    Console.WriteLine(ans);
}

void StepTwo()
{
    var forest = new List<List<int>>();
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var column = new List<int>(line.Length);
        foreach (var l in line)
        {
            column.Add(l - '0');
        }
        forest.Add(column);
    }
    var max = 0;
    var dir = new (int x, int y)[4] { (0, -1), (0, 1), (1, 0), (-1, 0) };
    for (int i = 1; i < forest.Count - 1; i++)
        for (int j = 1; j < forest[0].Count - 1; j++)
        {
            var h = forest[i][j];
            var curr = 1;
            for (int d = 0; d < 4; d++)
            {
                int x = i;
                int y = j;
                var step = 0;
                do
                {
                    x += dir[d].x;
                    y += dir[d].y;
                    if (x < 0 || y < 0 || x == forest.Count || y == forest[0].Count)
                        break;
                    if (forest[x][y] >= h) { step++; break; }
                    if (forest[x][y] < h) step++;
                } while (true);
                curr *= step;
            }
            max = Math.Max(max, curr);
        }
    Console.WriteLine(max);
}
StepOne();
StepTwo();
void StepOne()
{
    var N = 170;
    var M = 300;
    var offset = 400;
    var cave = new char[N][];
    for (int i = 0; i < N; i++)
    {
        cave[i] = new char[M];
        Array.Fill(cave[i], '.');
    }
    var minx = int.MaxValue;
    var maxx = int.MinValue;
    var maxy = int.MinValue;
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var split = line.Split(" -> ");
        for (int i = 0; i < split.Length - 1; i++)
        {
            var v = split[i].Split(",");
            var y = int.Parse(v[0]) - offset;
            var x = int.Parse(v[1]);
            var nextv = split[i + 1].Split(",");
            var nexty = int.Parse(nextv[0]) - offset;
            var nextx = int.Parse(nextv[1]);
            if (nextx == x)
            {
                if (y > nexty)
                    (y, nexty) = (nexty, y);
                for (int j = y; j <= nexty; j++)
                    cave[x][j] = '#';
            }
            else
            {
                if (x > nextx)
                    (x, nextx) = (nextx, x);
                for (int j = x; j <= nextx; j++)
                    cave[j][y] = '#';
            }

            minx = Math.Min(minx, x);
            minx = Math.Min(minx, nextx);

            maxx = Math.Max(maxx, x);
            maxx = Math.Max(maxx, nextx);
            maxy = Math.Max(maxy, y);
            maxy = Math.Max(maxy, nexty);
        }
    }
    Vis(cave);

    var sands = 0;
    while (true)
    {

        var sandy = 500 - offset;
        var sandx = 0;
        while (true)
        {
            if (cave[sandx + 1][sandy] == '.')
                sandx++;
            else
            {
                if (cave[sandx + 1][sandy - 1] == '.')
                {
                    sandx++;
                    sandy--;
                }
                else if (cave[sandx + 1][sandy + 1] == '.')
                {
                    sandy++;
                    sandx++;
                }
                else
                {
                    cave[sandx][sandy] = 'o';
                    sands++;
                    break;
                }
            }
            if (sandy <= 0 || sandy >= M || sandx + 1 == N)
            {
                Console.WriteLine(sands);
                return;
            }

        }
        Vis(cave);
    }


}

void StepTwo()
{
    var N = 171;
    var M = 1000;
    var offset = 0;
    var cave = new char[N][];
    for (int i = 0; i < N; i++)
    {
        cave[i] = new char[M];
        Array.Fill(cave[i], '.');
    }
    var minx = int.MaxValue;
    var maxx = int.MinValue;
    var maxy = int.MinValue;
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var split = line.Split(" -> ");
        for (int i = 0; i < split.Length - 1; i++)
        {
            var v = split[i].Split(",");
            var y = int.Parse(v[0]) - offset;
            var x = int.Parse(v[1]);
            var nextv = split[i + 1].Split(",");
            var nexty = int.Parse(nextv[0]) - offset;
            var nextx = int.Parse(nextv[1]);
            if (nextx == x)
            {
                if (y > nexty)
                    (y, nexty) = (nexty, y);
                for (int j = y; j <= nexty; j++)
                    cave[x][j] = '#';
            }
            else
            {
                if (x > nextx)
                    (x, nextx) = (nextx, x);
                for (int j = x; j <= nextx; j++)
                    cave[j][y] = '#';
            }

            minx = Math.Min(minx, x);
            minx = Math.Min(minx, nextx);

            maxx = Math.Max(maxx, x);
            maxx = Math.Max(maxx, nextx);
            maxy = Math.Max(maxy, y);
            maxy = Math.Max(maxy, nexty);
        }
    }

    for (int j = 0; j < M; j++)
        cave[maxx + 2][j] = '#';
    Vis(cave);

    var sands = 0;
    while (true)
    {

        var sandy = 500 - offset;
        var sandx = 0;

        while (true)
        {
            if (cave[sandx + 1][sandy] == '.')
                sandx++;
            else
            {
                if (cave[sandx + 1][sandy - 1] == '.')
                {
                    sandx++;
                    sandy--;
                }
                else if (cave[sandx + 1][sandy + 1] == '.')
                {
                    sandy++;
                    sandx++;
                }
                else
                {
                    cave[sandx][sandy] = 'o';
                    sands++;
                    if (sandx == 0)
                    {
                        Console.WriteLine(sands);
                        return;
                    }

                    break;
                }
            }
        }
    }


}

void Vis(char[][] cave)
{
    for (int i = 0; i < cave.Length; i++)
    {
        Console.WriteLine(string.Join("", cave[i].Select(x => x)));
    }
}
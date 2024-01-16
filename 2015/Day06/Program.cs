PartOne();
PartTwo();
void PartOne()
{
    var data = Parse();
    var arr = new bool[1000, 1000];
    foreach (var instruction in data)
    {
        for (int x = instruction.startx; x <= instruction.endx; x++)
            for (int y = instruction.starty; y <= instruction.endy; y++)
            {
                if (instruction.op == -1)
                    arr[x, y] = !arr[x, y];
                else
                    arr[x, y] = instruction.op == 1;
            }
    }
    var ans = 0;
    for (int x = 0; x < 1000; x++)
        for (int y = 0; y < 1000; y++)
            if (arr[x, y]) ans++;
    Console.WriteLine(ans);
}

void PartTwo()
{
    var data = Parse();
    var arr = new long[1000, 1000];
    foreach (var instruction in data)
    {
        for (int x = instruction.startx; x <= instruction.endx; x++)
            for (int y = instruction.starty; y <= instruction.endy; y++)
            {
                if (instruction.op == -1)
                    arr[x, y] += 2;
                else if (instruction.op == 1)
                    arr[x, y]++;
                else arr[x, y] = Math.Max(arr[x, y] - 1, 0);
            }
    }
    var ans = 0L;
    for (int x = 0; x < 1000; x++)
        for (int y = 0; y < 1000; y++)
            ans += arr[x, y];
    Console.WriteLine(ans);
}

List<(int startx, int starty, int endx, int endy, int op)> Parse()
{
    var lines = File.ReadAllLines("input.txt");
    List<(int startx, int starty, int endx, int endy, int sign)> res = new List<(int startx, int starty, int endx, int endy, int sign)>();
    foreach (var line in lines)
    {
        if (line[1] == 'o')
        {
            //toggle 914,643 through 975,840
            var nums = line.Substring(6).Split(" through ");
            var start = nums[0].Split(",").Select(x => int.Parse(x)).ToArray();
            var end = nums[1].Split(",").Select(x => int.Parse(x)).ToArray();
            res.Add((start[0], start[1], end[0], end[1], -1));
        }
        else if (line[6] == 'n')
        {
            //turn on 266,982 through 436,996
            var nums = line.Substring(7).Split(" through ");
            var start = nums[0].Split(",").Select(x => int.Parse(x)).ToArray();
            var end = nums[1].Split(",").Select(x => int.Parse(x)).ToArray();
            res.Add((start[0], start[1], end[0], end[1], 1));
        }
        else
        {
            //turn off 630,517 through 905,654
            var nums = line.Substring(8).Split(" through ");
            var start = nums[0].Split(",").Select(x => int.Parse(x)).ToArray();
            var end = nums[1].Split(",").Select(x => int.Parse(x)).ToArray();
            res.Add((start[0], start[1], end[0], end[1], 0));
        }
    }
    return res;
}

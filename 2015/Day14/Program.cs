PartOne();
PartTwo();
void PartOne()
{
    var ans = 0L;
    var data = Parse("input.txt");
    foreach (var deer in data)
    {
        ans = Math.Max(ans, Model(deer.speed, deer.time, deer.rest, 2503));
    }
    Console.WriteLine(ans);
}

long Model(int speed, int time, int rest, int limit)
{
    var ans = 0;
    var fly = time;
    for (int i = 1; i <= limit; i++)
    {
        if (fly > 0)
        {
            ans += speed;
            fly--;
        }
        else
        {
            fly = time;
            i += (rest - 1);
        }
    }
    return ans;
}
long ModelAll(List<(int speed, int time, int rest)> deers, int limit)
{
    var rest = new int[deers.Count];
    var run = new int[deers.Count];
    for (int i = 0; i < deers.Count; i++)
        run[i] = deers[i].time;
    var pos = new int[deers.Count];
    var points = new int[deers.Count];
    for (int time = 1; time <= limit; time++)
    {
        for (int i = 0; i < deers.Count; i++)
            if (rest[i] > 0)
                rest[i]--;
            else
            {
                run[i]--;
                if (run[i] == 0)
                {
                    rest[i] = deers[i].rest;
                    run[i] = deers[i].time;
                }
                pos[i] += deers[i].speed;
            }
        var maxPos = 0;
        for (int i = 1; i < deers.Count; i++)
            if (pos[maxPos] < pos[i])
                maxPos = i;
        points[maxPos]++;
    }
    return points.Max();
}


void PartTwo()
{
    var ans = 0L;
    var data = Parse("input.txt");

    ans = ModelAll(data, 2503);
    Console.WriteLine(ans);
}
List<(int speed, int time, int rest)> Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    var res = new List<(int speed, int time, int rest)>();
    foreach (var line in lines)
    {
        var split = line.Split(" seconds, but then must rest for ");
        var rest = int.Parse(split[1].Split(" ")[0]);
        var sp = split[0].Split(" km/s for ");
        var time = int.Parse(sp[1]);
        var br = sp[0].Split(" can fly ");
        var speed = int.Parse(br[1]);
        res.Add((speed, time, rest));
    }
    return res;

}
//StepOne();
StepTwo();
void StepOne()
{
    var l = new List<Sensor>();
    foreach (var str in File.ReadAllLines("input.txt"))
    {
        l.Add(new Sensor(str));
    }
    var line = 2000000;
    var x = l.First(x => x.BY == line).BX;
    var left = x - 1;
    while (true)
    {
        var stop = 0;
        foreach (var s in l)
            if (s.NotClosest(left, line))
            {
                stop++;
            }
        if (stop == l.Count)
        {
            left++;
            break;
        }
        else
            left--;
    }
    var right = x + 1;
    while (true)
    {
        var stop = 0;
        foreach (var s in l)
            if (s.NotClosest(right, line))
            {
                stop++;
            }
        if (stop == l.Count)
        {
            right--;
            break;
        }
        else
            right++;
    }
    Console.WriteLine(right - left);
}

void StepTwo()
{
    var l = new List<Sensor>();
    foreach (var str in File.ReadAllLines("input.txt"))
    {
        l.Add(new Sensor(str));

    }
    foreach (var s in l)
        if (s.NotClosest(14, 11))
        {
        }
    var max = 4000000;
    foreach (var sensor in l)
    {
        var dist = sensor.Distance + 1;
        for (int i = -dist; i < dist; i++)
        {
            var x = sensor.X + i;
            var y = sensor.Y - (dist - i);
            if (0 <= x && x <= max && 0 <= y && y <= max)
                if (l.All(s => s.NotClosest(x, y))){
                    Console.WriteLine(((long)x)*max+ y);
                    return;
                }
            y = sensor.Y + (dist - i);
            if (0 <= x && x <= max && 0 <= y && y <= max)
                if (l.All(s => s.NotClosest(x, y))){
                    Console.WriteLine(((long)x)*max+ y);
                    return;
                }
        }
    }


}


class Sensor
{
    public int X;
    public int Y;

    public int BX;
    public int BY;
    public int Distance;
    public Sensor(string str)
    {
        //Sensor at x=2, y=18: closest beacon is at x=-2, y=15
        var split = str.Split(":");
        var sensor = split[0].Split(",");
        X = int.Parse(sensor[0].Substring("Sensor at x=".Length));
        Y = int.Parse(sensor[1].Substring(" y=".Length));
        var beacon = split[1].Split(",");
        BX = int.Parse(beacon[0].Substring(" closest beacon is at x=".Length));
        BY = int.Parse(beacon[1].Substring(" y=".Length));
        Distance = Math.Abs(X - BX) + Math.Abs(Y - BY);
    }

    public bool NotClosest(int bx, int by)
    {
        return (Math.Abs(bx - X) + Math.Abs(by - Y)) > Distance;
    }
}



PartOne();
PartTwo();

void PartOne()
{
    var ans = 0L;
    var data = Parse("input.txt");
    ans = Count(data.formulas, data.start);
    Console.WriteLine(ans);
}

long Count(List<(string from, string to)> formulas, string start)
{
    HashSet<string> set = new HashSet<string>();
    foreach (var formula in formulas)
    {
        var pos = -1;
        while (true)
        {
            pos = start.IndexOf(formula.from, pos + 1);
            if (pos == -1)
                break;
            else
                set.Add(start.Substring(0, pos) + formula.to + start.Substring(pos + formula.from.Length));
        }

    }
    return set.Count;
}

void PartTwo()
{
    var ans = 0L;
    var data = Parse("input.txt");
    ans = WalkBack(data.formulas, data.start);
    Console.WriteLine(ans);

}
long WalkBack(List<(string from, string to)> list, string start)
{
    var target = start;
    var ans = 0;
    while (target != "e")
    {
        var temp = target;
        foreach (var formula in list)
        {
            var pos = target.IndexOf(formula.to, 0);
            if (pos == -1)
                continue;
            target = target.Substring(0, pos) + formula.from + target.Substring(pos + formula.to.Length);
            ans++;
        }

    }
    return ans;
}

long Reduce(List<(string from, string to)> list, string start)
{
    var ans = 0;
    var pq = new PriorityQueue<(string s, int cost), int>();
    var set = new HashSet<string>();
    pq.Enqueue((start, 0), start.Length);
    set.Add(start);
    while (pq.Count != 0)
    {
        var cur = pq.Dequeue();
        var stop = false;
        if (cur.s == "e")
            return cur.cost;
        foreach (var formula in list)
        {
            var pos = -1;
            while (true)
            {
                pos = cur.s.IndexOf(formula.to, pos + 1);
                if (pos == -1)
                    break;
                else
                {
                    var news = cur.s.Substring(0, pos) + formula.from + cur.s.Substring(pos + formula.to.Length);
                    if (set.Add(news))
                    {
                        pq.Enqueue((news, cur.cost + 1), news.Length);
                        stop = true;
                        break;
                    }
                }
            }
            if (stop)
                break;
        }
    }
    return -1;
}

(List<(string from, string to)> formulas, string start) Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    List<(string from, string to)> ans = new List<(string from, string to)>();
    for (int i = 0; i < lines.Length - 2; i++)
    {
        var sp = lines[i].Split(" => ");
        ans.Add((sp[0], sp[1]));
    }
    return (ans, lines[^1]);
}

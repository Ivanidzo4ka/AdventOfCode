PartOne();
PartTwo();

void PartOne()
{
    var ans = long.MaxValue;
    var edges = Parse("input.txt");
    var set = new HashSet<string>();
    foreach (var edge in edges)
        set.Add(edge.from);
    foreach (var start in set.ToList())
    {
        set.Remove(start);
        ans = Math.Min(ans,MinWalk(start, edges, set, 0));
        set.Add(start);
    }
    Console.WriteLine(ans);
}

long MinWalk(string start, List<(string from, string to, int cost)> edges, HashSet<string> set, int cost)
{
    if (set.Count == 0)
        return cost;
    var ans = long.MaxValue;
    foreach (var edge in edges)
    {
        if (edge.from == start && set.Contains(edge.to))
        {
            set.Remove(edge.to);
            ans = Math.Min(ans, MinWalk(edge.to, edges, set, cost + edge.cost));
            set.Add(edge.to);
        }
    }
    return ans;

}
long MaxWalk(string start, List<(string from, string to, int cost)> edges, HashSet<string> set, int cost)
{
    if (set.Count == 0)
        return cost;
    var ans = 0l;
    foreach (var edge in edges)
    {
        if (edge.from == start && set.Contains(edge.to))
        {
            set.Remove(edge.to);
            ans = Math.Max(ans, MaxWalk(edge.to, edges, set, cost + edge.cost));
            set.Add(edge.to);
        }
    }
    return ans;

}
void PartTwo()
{
    var ans =0l;
    var edges = Parse("input.txt");
    var set = new HashSet<string>();
    foreach (var edge in edges)
        set.Add(edge.from);
    foreach (var start in set.ToList())
    {
        set.Remove(start);
        ans = Math.Max(ans,MaxWalk(start, edges, set, 0));
        set.Add(start);
    }
    Console.WriteLine(ans);
}

List<(string from, string to, int cost)> Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    var edges = new List<(string from, string to, int cost)>();
    foreach (var line in lines)
    {
        var cSplit = line.Split(" = ");
        var cost = int.Parse(cSplit[1]);
        var dist = cSplit[0].Split(" to ");
        edges.Add((dist[0], dist[1], cost));
        edges.Add((dist[1], dist[0], cost));

    }
    return edges;
}
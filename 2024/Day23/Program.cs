using System.Text;
var dir = new (int x, int y)[] { (0, 1), (1, 0), (-1, 0), (0, -1) };
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
    var nodes = data.Keys.ToList();
    for (int i = 0; i < nodes.Count; i++)
    {

        var a1 = data[nodes[i]];
        for (int j = i + 1; j < nodes.Count; j++)
        {
            if (!a1.Contains(nodes[j])) continue;
            var a2 = data[nodes[j]];

            for (int k = j + 1; k < nodes.Count; k++)
            {
                if (!a2.Contains(nodes[k])) continue;
                if (data[nodes[k]].Contains(nodes[i]))
                {
                    if (nodes[i].StartsWith('t') || nodes[j].StartsWith('t') || nodes[k].StartsWith('t'))
                        ans++;
                }
            }
        }

    }

    Console.WriteLine(ans);
}

void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    var nodes = data.Keys.ToList();
    var cliques = FindClique(data, new HashSet<string>(), data.Keys.ToHashSet(), new HashSet<string>());
    var max = cliques.Max(x => x.Count);
    foreach (var clique in cliques)
    {
        if (clique.Count == max)
            Console.WriteLine(string.Join(",", clique.OrderBy(x => x).Select(x => x)));
    }
    
}
List<HashSet<string>> FindClique(Dictionary<string, HashSet<string>> edges, HashSet<string> cur, HashSet<string> candidates, HashSet<string> processed)
{
    var ans = new List<HashSet<string>>();
    if (candidates.Count == 0 && processed.Count == 0)
    {
        ans.Add(cur.ToHashSet());
    }
    foreach (var next in candidates.ToList())
    {
        var newCur = cur.ToHashSet();
        newCur.Add(next);
        var newCand = candidates.ToHashSet();
        newCand.IntersectWith(edges[next]);
        var newProc = processed.ToHashSet();
        newProc.IntersectWith(edges[next]);
        ans.AddRange(FindClique(edges, newCur, newCand, newProc));
        candidates.Remove(next);
        processed.Add(next);
    }
    return ans;
}

Dictionary<string, HashSet<string>> ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var edges = new Dictionary<string, HashSet<string>>();
    foreach (var line in lines)
    {
        var edge = line.Split("-");
        if (!edges.TryGetValue(edge[0], out var o))
        {
            o = new HashSet<string>();
            edges[edge[0]] = o;
        }
        o.Add(edge[1]);
        if (!edges.TryGetValue(edge[1], out o))
        {
            o = new HashSet<string>();
            edges[edge[1]] = o;
        }
        o.Add(edge[0]);

    }
    return edges;
}
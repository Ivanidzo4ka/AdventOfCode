PartOne();
PartTwo();

void PartOne()
{
    var ans = 0l;
    var data = ParseOne();
    ans = Process(data.ops, data.mentions);
    Console.WriteLine(ans);
    data.ops.Add(new Operation("NOOP", new[] { $"{ans}" }, "b"));
    ans = Process(data.ops, data.mentions);
    Console.WriteLine(ans);
}

void PartTwo()
{
    var ans = 0l;
    Console.WriteLine(ans);
}
long Process(List<Operation> ops, Dictionary<string, List<int>> mentions)
{
    var state = new Dictionary<string, int>();
    var queue = new Queue<int>();
    var processed = new bool[ops.Count];
    for (int i = 0; i < ops.Count; i++)
    {
        foreach (var p in ops[i].param)
        {
            if (int.TryParse(p, out var val))
            {
                state[p] = val;
            }
        }
        if (ops[i].Op == "NOOP")
        {
            queue.Enqueue(i);
        }
    }
    while (queue.Count != 0)
    {
        var cur = queue.Dequeue();
        if (processed[cur])
            continue;
        bool paramsResolved = true;
        foreach (var p in ops[cur].param)
            if (!state.ContainsKey(p))
                paramsResolved = false;
        if (!paramsResolved) continue;
        var ans = 0;
        ans = ops[cur].Op switch
        {
            "AND" => state[ops[cur].param[0]] & state[ops[cur].param[1]],
            "OR" => state[ops[cur].param[0]] | state[ops[cur].param[1]],
            "LSHIFT" => state[ops[cur].param[0]] << state[ops[cur].param[1]],
            "RSHIFT" => state[ops[cur].param[0]] >> state[ops[cur].param[1]],
            "NOT" => 65535 - state[ops[cur].param[0]],
            "NOOP" => state[ops[cur].param[0]],
        };
        if (mentions.TryGetValue(ops[cur].activate, out var mentionList))
            foreach (var mention in mentionList)
                queue.Enqueue(mention);

        state[ops[cur].activate] = ans;
        processed[cur] = true;
    }
    return state["a"];
}
(List<Operation> ops, Dictionary<string, List<int>> mentions) ParseOne()
{
    var lines = File.ReadAllLines("input.txt");
    var ops = new List<Operation>();
    var mentions = new Dictionary<string, List<int>>();
    foreach (var line in lines)
    {
        var split = line.Split(" -> ");
        var activate = split[1];
        if (split[0].Contains(" AND "))
        {
            var d = split[0].Split(" AND ");
            var op = new Operation("AND", new[] { d[0], d[1] }, activate);
            ops.Add(op);
        }
        else if (split[0].Contains(" OR "))
        {
            var d = split[0].Split(" OR ");
            var op = new Operation("OR", new[] { d[0], d[1] }, activate);
            ops.Add(op);
        }
        else if (split[0].Contains(" LSHIFT "))
        {
            var d = split[0].Split(" LSHIFT ");
            var op = new Operation("LSHIFT", new[] { d[0], d[1] }, activate);
            ops.Add(op);
        }
        else if (split[0].Contains(" RSHIFT "))
        {
            var d = split[0].Split(" RSHIFT ");
            var op = new Operation("RSHIFT", new[] { d[0], d[1] }, activate);
            ops.Add(op);
        }
        else if (split[0].Contains("NOT "))
        {
            var op = new Operation("NOT", new[] { split[0].Substring(4) }, activate);
            ops.Add(op);
        }
        else
        {
            var op = new Operation("NOOP", new[] { split[0] }, activate);
            ops.Add(op);
        }
    }
    for (int i = 0; i < ops.Count; i++)
    {
        foreach (var p in ops[i].param)
        {
            if (!mentions.TryGetValue(p, out var val))
            {
                val = new List<int>();
                mentions[p] = val;
            }
            val.Add(i);
        }
    }
    return (ops, mentions);
}
public record Operation(string Op, string[] param, string activate);



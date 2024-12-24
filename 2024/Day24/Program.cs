PartOne(true);
PartOne(false);
//PartTwo(true);
PartTwo(false);
void PartOne(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;

    if (test)
        Console.Write("Test:");
    while (true)
    {
        bool updated = false;
        foreach (var rule in data.rules)
        {
            if (data.vals.ContainsKey(rule.result)) continue;
            if (data.vals.TryGetValue(rule.left, out var leftVal) && data.vals.TryGetValue(rule.right, out var rightVal))
            {
                bool res = false;
                if (rule.op == "XOR")
                {
                    res = leftVal ^ rightVal;
                }
                else if (rule.op == "AND")
                    res = leftVal & rightVal;
                else res = leftVal | rightVal;
                updated = true;
                data.vals[rule.result] = res;
            }
        }
        if (!updated)
            break;
    }
    foreach (var kvp in data.vals)
    {
        if (kvp.Key.StartsWith("z"))
        {
            var val = kvp.Value ? 1L : 0L;
            ans += val << (int.Parse(kvp.Key.Substring(1)));
        }
    }
    Console.WriteLine(ans);
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var backMap = new Dictionary<string, string>();
    var forthMap = new Dictionary<string, string>();
    foreach (var pair in data.rules)
    {
        var sorted = "";
        if (pair.left.CompareTo(pair.right) < 0)
        {
            sorted = $"{pair.left} {pair.op} {pair.right}";
        }
        else sorted = $"{pair.right} {pair.op} {pair.left}";
        backMap[sorted] = pair.result;
        forthMap[pair.result] = sorted;
    }
    var overFlow = "";
    for (int i = 1; i <= 44; i++)
    {
        var pastOverFlowWire = "";
        var curXor = $"x{i.ToString("d2")} XOR y{i.ToString("d2")}";
        var curXorWire = backMap[curXor];
        Console.WriteLine($"{curXor} -> {curXorWire}");
        if (overFlow == "")
        {
            var prevAnd = $"x{(i - 1).ToString("d2")} AND y{(i - 1).ToString("d2")}";
            pastOverFlowWire = backMap[prevAnd];
            Console.WriteLine($"{prevAnd} -> {pastOverFlowWire}");
        }
        else
        {
            var prevAnd = $"x{(i - 1).ToString("d2")} AND y{(i - 1).ToString("d2")}";
            var prevWire = backMap[prevAnd];
            Console.WriteLine($"{prevAnd} -> {prevWire}");
            var overFlowWire = backMap[overFlow];
            Console.WriteLine($"{overFlow} -> {overFlowWire}");
            if (overFlowWire.CompareTo(prevWire) > 0)
            {
                (overFlowWire, prevWire) = (prevWire, overFlowWire);
            }
            var pastOverFlow = $"{overFlowWire} OR {prevWire}";
            pastOverFlowWire = backMap[pastOverFlow];
            Console.WriteLine($"{pastOverFlow} -> {pastOverFlowWire}");
        }

        if (pastOverFlowWire.CompareTo(curXorWire) > 0)
        {
            (pastOverFlowWire, curXorWire) = (curXorWire, pastOverFlowWire);
        }

        var sorted = $"{pastOverFlowWire} XOR {curXorWire}";
        Console.WriteLine($"{sorted} -> z{i.ToString("d2")}");
        if (forthMap[$"z{i.ToString("d2")}"] != sorted)
        {
            Console.WriteLine($"Need to fix z: {sorted}");
        }
        Console.WriteLine();
        overFlow = $"{pastOverFlowWire} AND {curXorWire}";

    }
    var allSwaps = new List<IList<int>>();
    var entropy = Validate(44, data.rules);
    if (test)
        Console.Write("Test:");
    var ans = new List<string>{"gdd","z05","cwt","z09","css","jmv","pqt","z37"};
    Console.WriteLine(string.Join(",", ans.OrderBy(x=>x)));
}

Dictionary<string, bool> GetEmpty(int num)
{
    var vals = new Dictionary<string, bool>();
    for (int i = 0; i <= num; i++)
    {
        vals[$"x{i.ToString("D2")}"] = false;
        vals[$"y{i.ToString("D2")}"] = false;
    }
    return vals;
}
int Validate(int num, List<(string left, string right, string op, string result)> rules)
{
    var entropy = 0;
    for (int c = 0; c < num; c++)
    {
        // 0 + 0 = all 0
        var vals = GetEmpty(num);
        Run(vals, rules);
        for (int i = 0; i < num; i++)
        {
            if (vals[$"z{i.ToString("D2")}"])
                entropy++;
        }
        // 1+0 == all 0 expect 1
        vals = GetEmpty(num);
        vals[$"x{c.ToString("D2")}"] = true;
        Run(vals, rules);
        for (int i = 0; i < num; i++)
        {
            if (i == c)
            {
                if (!vals[$"z{c.ToString("D2")}"]) entropy++;
            }
            else
            if (vals[$"z{i.ToString("D2")}"]) entropy++;
        }
        // 0 + 1 = all 0 except c =1
        vals = GetEmpty(num);
        vals[$"y{c.ToString("D2")}"] = true;
        Run(vals, rules);
        for (int i = 0; i < num; i++)
        {
            if (i == c)
            {
                if (!vals[$"z{c.ToString("D2")}"]) entropy++;
            }
            else
            if (vals[$"z{i.ToString("D2")}"]) entropy++;
        }

        vals = GetEmpty(num);
        // 1+1 all 0 except c+1 = 1;
        vals[$"x{c.ToString("D2")}"] = true;
        vals[$"y{c.ToString("D2")}"] = true;

        Run(vals, rules);
        for (int i = 0; i < num; i++)
        {
            if (i == c + 1)
            {
                if (!vals[$"z{i.ToString("D2")}"]) entropy++;
            }
            else
            if (vals[$"z{i.ToString("D2")}"]) entropy++;
        }
    }
    return entropy;
}

void Run(Dictionary<string, bool> vals, List<(string left, string right, string op, string result)> rules)
{
    while (true)
    {
        bool updated = false;
        foreach (var rule in rules)
        {
            if (vals.ContainsKey(rule.result)) continue;
            if (vals.TryGetValue(rule.left, out var leftVal) && vals.TryGetValue(rule.right, out var rightVal))
            {
                bool res = false;
                if (rule.op == "XOR")
                {
                    res = leftVal ^ rightVal;
                }
                else if (rule.op == "AND")
                    res = leftVal & rightVal;
                else res = leftVal | rightVal;
                updated = true;
                vals[rule.result] = res;
            }
        }
        if (!updated)
            break;
    }
}

(Dictionary<string, bool> vals, List<(string left, string right, string op, string result)> rules) ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var pos = 0;
    var vals = new Dictionary<string, bool>();
    while (lines[pos] != "")
    {
        var sp = lines[pos].Split(": ");
        vals[sp[0]] = sp[1] == "1";
        pos++;
    }
    pos++;
    var rules = new List<(string left, string right, string op, string result)>();
    while (pos < lines.Length)
    {
        var sp = lines[pos].Split(" -> ");
        var elem = sp[0].Split(" ");
        rules.Add((elem[0], elem[2], elem[1], sp[1]));
        pos++;
    }
    return (vals, rules);
}
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
    foreach (var page in data.pages)
    {
        if (IsGood(page, data.rules))
            ans += page[page.Count / 2];
    }
    Console.WriteLine(ans);
}
bool IsGood(List<int> page, List<int[]> rules)
{
    var set = new HashSet<int>();
    for (int i = 0; i < page.Count; i++)
    {
        foreach (var rule in rules)
        {
            if (rule[0] == page[i])
            {
                if (set.Contains(rule[1])) return false;
            }
        }
        set.Add(page[i]);
    }
    return true;

}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    foreach (var page in data.pages)
    {
        if (!IsGood(page, data.rules))
        {
            Reorder(page, data.rules);
            ans += page[page.Count / 2];
        }
    }
    Console.WriteLine(ans);
}
void Reorder(List<int> page, List<int[]> rules)
{
    var newOne = new List<int>();
    var set = page.ToHashSet();
    while (newOne.Count != page.Count)
    {
        var counts = new Dictionary<int, int>();
        foreach (var elem in set)
        {
            foreach (var rule in rules)
            {
                if (rule[1] == elem && set.Contains(rule[0]))
                {
                    counts.TryGetValue(elem, out var count);
                    counts[elem] = count + 1;
                }
            }
        }
        foreach (var elem in set)
        {
            if (!counts.ContainsKey(elem))
            {
                newOne.Add(elem);
                set.Remove(elem);
            }
        }
    }
    page.Clear();
    foreach (var elem in newOne)
        page.Add(elem);

}

(List<int[]> rules, List<List<int>> pages) ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var pos = 0;
    var rules = new List<int[]>();
    var pages = new List<List<int>>();
    while (lines[pos] != "")
    {
        rules.Add(lines[pos].Split("|").Select(x => int.Parse(x)).ToArray());
        pos++;
    }
    pos++;
    while (pos < lines.Length)
    {
        pages.Add(lines[pos].Split(",").Select(x => int.Parse(x)).ToList());
        pos++;
    }
    return (rules, pages);
}
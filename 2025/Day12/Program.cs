PartOne();
void PartOne(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
    {
        Console.Write("Test:");
    }
    for (int i = 0; i < data.area.Count; i++)
    {
        var need = data.dim[i].Sum();
        if (need <= (data.area[i].h / 3) * (data.area[i].w / 3))
            ans++;
    }
    Console.WriteLine(ans);
}

(List<(int h, int w)> area, List<IList<int>> dim) ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var area = new List<(int h, int w)>();
    var dim = new List<IList<int>>();
    for (int i = 30; i < lines.Length; i++)
    {
        var t = lines[i].Split(':', StringSplitOptions.RemoveEmptyEntries);
        var d = t[0].Split('x');
        area.Add((int.Parse(d[0]), int.Parse(d[1])));
        dim.Add(t[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());
    }
    return (area, dim);
}

PartOne();
PartTwo();

void PartOne()
{
    var ans = 0L;
    var data = Parse("input.txt");
    Dictionary<string, int> lookup = new Dictionary<string, int>()
    {
        ["children"] = 3,
        ["cats"] = 7,
        ["samoyeds"] = 2,
        ["pomeranians"] = 3,
        ["akitas"] = 0,
        ["vizslas"] = 0,
        ["goldfish"] = 5,
        ["trees"] = 3,
        ["cars"] = 2,
        ["perfumes"] = 1,
    };
    for (int i = 0; i < data.Count; i++)
    {
        var good = true;
        foreach (var obj in data[i])
            if (lookup[obj.Key] != obj.Value)
            {
                good = false;
                break;
            }
        if (good)
        {
            Console.WriteLine(i + 1);
            //break;
        }
    }
}

void PartTwo()
{
    var ans = 0L;
    var data = Parse("input.txt");
    Dictionary<string, (int val, int comp)> lookup = new Dictionary<string, (int val, int comp)>()
    {
        ["children"] = (3, 0),
        ["cats"] = (7, -1),
        ["samoyeds"] = (2, 0),
        ["pomeranians"] = (3, 1),
        ["akitas"] = (0, 0),
        ["vizslas"] = (0, 0),
        ["goldfish"] = (5, 1),
        ["trees"] = (3, -1),
        ["cars"] = (2, 0),
        ["perfumes"] = (1, 0),
    };
    for (int i = 0; i < data.Count; i++)
    {
        var good = true;
        foreach (var obj in data[i])
            if (lookup[obj.Key].val.CompareTo(obj.Value) != lookup[obj.Key].comp)
            {
                good = false;
                break;
            }
            else
            {

            }
        if (good)
        {
            Console.WriteLine(i + 1);
            //break;
        }
    }
}
List<Dictionary<string, int>> Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    var res = new List<Dictionary<string, int>>();
    foreach (var line in lines)
    {
        //Sue 9: trees: 2, vizslas: 7, samoyeds: 6
        var objs = line.Substring(line.IndexOf(':') + 2);
        var dic = new Dictionary<string, int>();
        foreach (var obj in objs.Split(", "))
        {
            var sp = obj.Split(":");
            dic[sp[0]] = int.Parse(sp[1]);
        }
        res.Add(dic);
    }
    return res;
}



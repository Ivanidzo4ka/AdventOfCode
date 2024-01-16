PartOne();
PartTwo();

void PartOne()
{
    var data = Parse("test.txt");
    var sum = data.Sum();
    var ans = PackSmallest(data, 0, sum / 3, 0, 1);
    Console.WriteLine(ans.prod);
}

(int size, long prod) PackSmallest(List<int> data, int pos, int desire, int size, long product)
{
    var found = (int.MaxValue, -1L);
    if (desire == 0)
        found = (size, product);
    else if (pos < data.Count && desire > 0)
    {

        var ans = PackSmallest(data, pos + 1, desire - data[pos], size + 1, product * data[pos]);
        var dontAdd = PackSmallest(data, pos + 1, desire, size, product);
        if (ans.size < dontAdd.size)
            found = ans;
        else if (ans.size > dontAdd.size)
            found = dontAdd;
        else if (ans.prod < dontAdd.prod)
            found = ans;
        else
            found = dontAdd;
    }
    return found;
}

void PartTwo()
{
    var data = Parse("input.txt");
    var sum = data.Sum();
    var ans = PackSmallest(data, 0, sum / 4, 0, 1);
    Console.WriteLine(ans.prod);
}

List<int> Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    return lines.Select(x => int.Parse(x)).ToList();
}


using System.Text;
using System.Text.RegularExpressions;

PartOne(true);
PartOne(false);
PartTwo(true);
PartTwo(false);
void PartOne(bool test = false)
{
    var data = ReadData(test);
    var ans = 0l;
    if (test)
        Console.Write("Test:");

    Regex regex = new Regex("(mul\\(\\d{1,3},\\d{1,3}\\))");

    foreach (Match match in regex.Matches(data))
    {

        var s = match.ToString();
        s = s.Substring(4, s.Length - 4);
        s = s.Substring(0, s.Length - 1);
        var z = 1l;
        foreach (var t in s.Split(",").Select(x => int.Parse(x)))
        {
            z *= t;
        }
        ans += z;
    }
    Console.WriteLine(ans);
}

void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    var prev = 0;
    var enabled = true;
    Regex regex = new Regex("(mul\\(\\d{1,3},\\d{1,3}\\))");
    foreach (Match match in regex.Matches(data))
    {
        var next = match.Index + match.Length;
        var s = match.ToString();
        s = s.Substring(4, s.Length - 4);
        s = s.Substring(0, s.Length - 1);
        var z = 1L;
        foreach (var t in s.Split(",").Select(x => int.Parse(x)))
        {
            z *= t;
        }

        var between = data.Substring(prev, match.Index - prev);
        var good = between.LastIndexOf("do()");
        var bad = between.LastIndexOf("don't()");
        prev = next;
        if (good == -1 && bad == -1)
        {

        }
        else if (good == -1 && bad != -1)
            enabled = false;
        else if (bad == -1 && good != -1)
            enabled = true;
        else enabled = good > bad;
        if (enabled)
            ans += z;
    }
    Console.WriteLine(ans);
}

string ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var sb = new StringBuilder();
    foreach (var line in lines)
        sb.AppendLine(line);
    return sb.ToString();
}
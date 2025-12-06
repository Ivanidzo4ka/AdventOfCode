using System.Globalization;
using Microsoft.VisualBasic;

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

    for (int i = 0; i < data.ops.Count; i++)
    {
        var t = 0L;
        if (data.ops[i] == "+")
        {
            for (int j = 0; j < data.data.Count; j++)
            {
                t += data.data[j][i];
            }

        }
        else
        {
            t = 1L;
            for (int j = 0; j < data.data.Count; j++)
            {
                t *= data.data[j][i];
            }

        }
        ans += t;
    }
    Console.WriteLine(ans);
}
void PartTwo(bool test = false)
{
    var data = ReadData2(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");

    for (int i = 0; i < data.ops.Count; i++)
    {
        var t = 0L;
        if (data.ops[i] == "+")
        {
            for (int j = 0; j < data.data[i].Count; j++)
            {
                t += data.data[i][j];
            }

        }
        else
        {
            t = 1L;
            for (int j = 0; j < data.data[i].Count; j++)
            {
                t *= data.data[i][j];
            }

        }
        ans += t;
    }
    Console.WriteLine(ans);
}
(List<List<long>> data, List<string> ops) ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var data = new List<List<long>>();
    for (int i = 0; i < lines.Length - 1; i++)
    {
        data.Add(lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList());
    }

    var ops = lines[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
    return (data, ops);
}

(List<List<long>> data, List<string> ops) ReadData2(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");

    var data = new List<List<long>>();
    var pos = -1;
    var ops = new List<string>();
    for (int i = 0; i < lines[^1].Length; i++)
    {
        if (lines[^1][i] != ' ')
        {
            ops.Add(lines[^1][i].ToString());
            pos++;
            data.Add(new List<long>()); 
        }
        var num = 0;
        for (int j = 0; j < lines.Length - 1; j++)
        {
            if (lines[j][i] != ' ')
            {
                num *= 10;
                num += lines[j][i] - '0';
            }
        }
        if (num == 0)
            continue;
        data[pos].Add(num);
    }


    return (data, ops);
}


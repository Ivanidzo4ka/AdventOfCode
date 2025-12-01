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
    var start = 50;
    foreach (var line in data)
    {
        if (line[0] == 'L')
        {
            start -= int.Parse(line[1..]);
        }
        else
        {
            start += int.Parse(line[1..]);
        }
        if (start < 0) start += 100;
        start %= 100;
        if (start == 0) ans++;

    }
    Console.WriteLine(ans);
}

void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    var start = 50;
    foreach (var line in data)
    {
        if (line[0] == 'L')
        {
            for (int i = 0; i < int.Parse(line[1..]); i++)
            {
                start--;
                if (start == 0) ans++;
                if (start < 0) start += 100;
            }
        }
        else
        {
            for (int i = 0; i < int.Parse(line[1..]); i++)
            {
                start++;

                if (start >= 100) start %= 100;
                if (start == 0) ans++;
            }
        }
    }
    Console.WriteLine(ans);
}
IList<string> ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");

    return lines;
}
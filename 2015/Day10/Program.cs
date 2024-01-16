using System.Text;

PartOne();
PartTwo();

void PartOne()
{
    var s = Parse();
    for (int i = 0; i < 40; i++)
        s = Transform(s);
    Console.WriteLine(s.Length);
}

string Transform(string s)
{
    var sb = new StringBuilder();
    var count = 1;
    for (int i = 1; i < s.Length; i++)
    {
        if (s[i] == s[i - 1])
            count++;
        else
        {
            sb.Append(count);
            sb.Append(s[i - 1]);
            count = 1;
        }
    }
    sb.Append(count);
    sb.Append(s[^1]);

    return sb.ToString();
}
void PartTwo()
{
    var ans = 0L;
    var s = Parse();
    for (int i = 0; i < 50; i++)
        s = Transform(s);

    Console.WriteLine(s.Length);
}


string Parse()
{
    var lines = File.ReadAllLines("input.txt");
    return lines[0];
}

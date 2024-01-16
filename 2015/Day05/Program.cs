PartOne();
PartTwo();

void PartOne()
{
    var ans = 0;
    var lines = Parse();
    foreach (var line in lines)
    {
        if (NiceString(line))
            ans++;
    }

    Console.WriteLine(ans);
}
bool NiceString(string s)
{
    var vowels = 0;

    for (int i = 0; i < s.Length; i++)
    {
        if ((s[i] is 'a' or 'u' or 'e' or 'i' or 'o'))
            vowels++;
    }
    if (vowels < 3)
        return false;
    var twice = 0;
    for (int i = 1; i < s.Length; i++)
    {
        if (s[i - 1] == s[i]) twice++;
        if (s[i - 1] == 'a' && s[i] == 'b') return false;
        if (s[i - 1] == 'c' && s[i] == 'd') return false;
        if (s[i - 1] == 'p' && s[i] == 'q') return false;
        if (s[i - 1] == 'x' && s[i] == 'y') return false;
    }
    if (twice == 0)
        return false;
    return true;
}
void PartTwo()
{
    var ans = 0;
    var lines = Parse();
    foreach (var line in lines)
    {
        if (NiceString2(line))
            ans++;
    }

    Console.WriteLine(ans);
}

bool NiceString2(string s)
{
    bool repeats = false;
    for (int i = 0; i < s.Length - 2; i++)
        if (s[i] == s[i + 2])
            repeats = true;
    if (!repeats)
        return false;
    bool pair = false;
    var set = new Dictionary<string, int>();
    for (int i = 0; i < s.Length - 1; i++)
    {
        var t = s.Substring(i, 2);
        if (set.TryGetValue(t, out var pos))
        {
            if (pos + 2 <= i)
                pair = true;
        }
        else set[t] = i;

    }
    return pair;

}

IList<string> Parse()
{
    var lines = File.ReadAllLines("input.txt");
    return lines;
}


PartOne();
PartTwo();
void PartOne()
{
    var ans = 0;
    var lines = File.ReadAllLines("input.txt");
    foreach (var line in lines)
    {
        if (IsTls(line))
        {
            ans++;
        }
    }
    Console.WriteLine(ans);
}
(List<string> norms, List<string> netx) BreakIt(string s)
{
    var origin = s;
    var nets = new List<string>();
    var norms = new List<string>();
    while (s.IndexOf('[') != -1)
    {
        var first = s.IndexOf('[');
        var last = s.IndexOf(']');
        if (last == -1)
        {
            break;
        }
        else
        {
            norms.Add(s.Substring(0, first));
            nets.Add(s.Substring(first + 1, last - first - 1));
            s = s.Substring(last + 1, s.Length - last - 1);
        }
    }
    norms.Add(s);
    return (nets, norms);
}

bool IsTls(string s)
{
    var (nets, norms) = BreakIt(s);
    foreach (var net in nets)
    {
        if (IsAbba(net)) return false;
    }
    foreach (var norm in norms)
    {
        if (IsAbba(norm)) return true;
    }
    return false;
}

bool IsAbba(string s)
{

    for (int i = 0; i < s.Length - 3; i++)
    {
        if (s[i] == s[i + 3] && s[i + 1] == s[i + 2] && s[i] != s[i + 1] && s[i] != '[' && s[i] != ']')
        {
            return true;
        }

    }
    return false;
}
void PartTwo()
{
    var ans = 0;
    var lines = File.ReadAllLines("input.txt");
    foreach (var line in lines)
    {

        if (IsSSL(line))
        {
            ans++;
        }
    }
    Console.WriteLine(ans);
}

bool IsSSL(string s)
{
    var (nets, norms) = BreakIt(s);
    for (char a = 'a'; a <= 'z'; a++)
        for (char b = 'a'; b <= 'z'; b++)
        {
            if(a==b) continue;
            string aba = $"{a}{b}{a}";
            string bab = $"{b}{a}{b}";
            if (nets.Any(a => a.Contains(bab)) && norms.Any(a => a.Contains(aba)))
                return true;
        }
    return false;

}

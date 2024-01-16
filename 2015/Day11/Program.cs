using System.Text;
PartOne();
PartTwo();


void PartOne()
{
    var ans = 0L;
    var data = Parse("input.txt");
    var password = data[0];
    while (true)
    {
        password = Increment(password);
        if (Valid(password))
            break;
    }
    Console.WriteLine(password);
}
bool Valid(string s)
{
    var first = false;
    for (int i = 0; i < s.Length - 2; i++)
        if (s[i]+1 == s[i + 1] && s[i + 1] + 1 == s[i + 2])
            first = true;
    if (!first) return false;
    var second = true;
    for (int i = 0; i < s.Length; i++)
        if (s[i] is 'i' or 'o' or 'l') second = false;
    if (!second) return false;
    var set = new HashSet<char>();
    for (int i = 0; i < s.Length - 1; i++)
        if (s[i] == s[i + 1])
        {
            set.Add(s[i]);
            i++;
        }
    return set.Count >= 2;
}
string Increment(string s)
{
    var sb = new StringBuilder();
    var over = 1;
    var pos = s.Length - 1;
    while (over != 0 || pos >= 0)
    {
        var l = pos >= 0 ? s[pos] : 'a' - 1;
        if (over == 1)
        {
            over = 0;
            l++;
            if (l > 'z') { l = 'a'; over = 1; }
        }
        sb.Insert(0, (char)l);
        pos--;
    }
    return sb.ToString();
}

void PartTwo()
{
   var ans = 0L;
    
    var password = "vzbxxyzz";
    while (true)
    {
        password = Increment(password);
        if (Valid(password))
            break;
    }
    Console.WriteLine(password);
}


IList<string> Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    return lines;
}

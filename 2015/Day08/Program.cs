using System.Text;
PartOne();
PartTwo();
void PartOne()
{
    var ans = 0;
    var lines = Parse("input.txt");
    foreach (var line in lines)
    {
        ans += line.Length - UnEscape(line).Length;
    }

    Console.WriteLine(ans);
}
void PartTwo()
{

    var ans = 0;
    var lines = Parse("input.txt");
    foreach (var line in lines)
    {
        ans += Pad(line);
    }
    Console.WriteLine(ans);
}
int Pad(string s)
{
    var ans = 2;
    for (int i = 0; i < s.Length; i++)
    {
        if (char.IsDigit(s[i]) || char.IsLetter(s[i]))
            continue;
        ans++;
    }
    return ans;
}
string UnEscape(string line)
{
    var sb = new StringBuilder();
    var pos = 1;

    while (pos < line.Length - 1)
    {
        sb.Append(Process(line, ref pos));
    }
    return sb.ToString();
}
char Process(string s, ref int pos)
{
    if (s[pos] != '\\')
    {
        pos++;
        return s[pos - 1];
    }
    else if (s[pos + 1] == 'x')
    {

        pos += 4;
        return 'A';
    }
    pos += 2;
    return s[pos - 1];

}



IList<string> Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    return lines;
}

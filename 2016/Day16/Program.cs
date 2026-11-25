
using System.Text;

Solve();
Solve(true);
void Solve(bool partTwo=false)
{
    var (s, len)= Parse("input.txt");
    if (partTwo) len = 35651584;
    while (s.Length<len)
    {
        s = Transform(s);
    }
    Console.WriteLine(CheckSum(s.Substring(0, len)));
}
string Transform(string a)
{
    var b = a.Reverse().Select(x=> {if (x=='1') return '0'; return '1';});
    var sb = new StringBuilder(a);
    sb.Append('0');
    foreach(var c in b)
    sb.Append(c);
    return sb.ToString();
}
string CheckSum(string a)
{
 var sb = new StringBuilder();;
 while (true)
    {
        for(int i=0; i<a.Length/2; i++)
        {
            if (a[2*i]==a[2*i+1]) sb.Append("1");
            else sb.Append("0");
        }
        if (sb.Length%2==0) {a =sb.ToString(); sb.Clear();}
        else return sb.ToString();
    }   
}
(string s, int len) Parse(string input)
{
    var lines = File.ReadAllLines(input);
    return (lines[0], int.Parse(lines[1]));
}
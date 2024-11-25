using System.Linq;
using System.Text;

PartOne();
PartTwo();
void PartOne()
{
    var lines = File.ReadAllLines("input.txt");
    var ans = 0;
    foreach (var line in lines)
    {
        var sp = line.Split('-');
        var freq = new Dictionary<char, int>();
        for (int i = 0; i < sp.Length - 1; i++)
        {
            foreach (var l in sp[i])
            {
                freq.TryGetValue(l, out var c);
                freq[l] = c + 1;
            }
        }

        var hash = sp[^1].Substring(sp[^1].Length - 6, 5);
        var num = int.Parse(sp[^1].Substring(0, sp[^1].Length - 7));
        var actualHash = string.Join("", freq.OrderByDescending(x => x.Value).ThenBy(x => x.Key).Take(5).Select(x => x.Key));
        if (hash == actualHash)
        {
            ans += num;
        }

    }
    Console.WriteLine(ans);
}
void PartTwo()
{
    var lines = File.ReadAllLines("input.txt");
    foreach (var line in lines)
    {
        var sp = line.Split('-');
        var num = int.Parse(sp[^1].Substring(0, sp[^1].Length - 7));
        var sb = new StringBuilder();
        for (int i = 0; i < sp.Length - 1; i++)
        {
            foreach (var l in sp[i])
            {
                sb.Append((char)(((l - 'a' + num) % 26) + 'a'));
            }
            sb.Append(' ');
        }
        if (sb.ToString() == "northpole object storage ")
            Console.WriteLine(num);
    }
}
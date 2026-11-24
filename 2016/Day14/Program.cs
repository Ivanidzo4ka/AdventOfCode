using System.Security.Cryptography;
using System.Text;
PartOne();
PartTwo();
void PartOne()
{
    var salt = Parse("input.txt");
    int pos = 0;
    Console.WriteLine(FindNext(salt, pos));
}
void PartTwo()
{
    var salt = Parse("input.txt");
    int pos = 0;
    Console.WriteLine(FindNext(salt, pos, true));
}
string GenerateMd5(string salt, int pos, bool partTwo = false)
{
    var md5 = MD5.Create();
    var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(salt + pos.ToString()));
    if (partTwo)
    {
        for (int z = 0; z < 2016; z++)
        {
            var aBuilder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                aBuilder.Append(hash[i].ToString("x2"));
            }
            hash = md5.ComputeHash(Encoding.ASCII.GetBytes(aBuilder.ToString()));
        }
    }
    var sBuilder = new StringBuilder();
    for (int i = 0; i < hash.Length; i++)
    {
        sBuilder.Append(hash[i].ToString("x2"));
    }
    return sBuilder.ToString();
}
char FirstTriplet(string t)
{
    for (int i = 2; i < t.Length; i++)
    {
        if (t[i] == t[i - 1] && t[i] == t[i - 2])
        {
            return t[i];
        }

    }
    return ' ';
}
bool GetFives(string t, char c)
{
    var ans = new List<char>(0);
    for (int i = 4; i < t.Length; i++)
    {
        if (c == t[i] && t[i] == t[i - 1] && t[i] == t[i - 2] && t[i] == t[i - 3] && t[i] == t[i - 4])
        {
            return true;
        }
    }
    return false;
}
int FindNext(string salt, int pos, bool partTwo = false)
{
    var ans = new List<int>();
    Dictionary<int, string> md5s = new Dictionary<int, string>();
    pos = 0;
    while (ans.Count < 64)
    {
        if (!md5s.ContainsKey(pos))
        {
            md5s[pos] = GenerateMd5(salt, pos, partTwo);
        }
        var t = md5s[pos];
        var c = FirstTriplet(t);
        if (c != ' ')
        {
            for (int j = 1; j < 1001; j++)
            {
                if (!md5s.ContainsKey(pos + j))
                {
                    md5s[pos + j] = GenerateMd5(salt, pos + j, partTwo);
                }
                var z = md5s[pos + j];
                if (GetFives(z, c))
                {
                    ans.Add(pos);
                    break;
                }
            }
        }
        pos++;
        md5s.Remove(pos - 1);
    }
    return ans[63];
}
string Parse(string input)
{
    return File.ReadAllText(input);
}
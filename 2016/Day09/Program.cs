PartOne();
PartTwo();
void PartOne()
{
    var lines = File.ReadAllLines("input.txt");
    foreach (var data in lines)
    {
        var start = -1;
        var ans = 0;
        for (int i = 0; i < data.Length; i++)
        {
            if (start == -1) ans++;
            if (data[i] == '(') {start = i;ans--;}
            else if (data[i] == ')')
            {
                var dig = data.Substring(start+1, i - start-1).Split("x").Select(x => int.Parse(x)).ToArray();
                start = -1;
                i += dig[0];
                ans += dig[0] * dig[1];
            }
        }
        Console.WriteLine(ans);
    }
}

void PartTwo()
{
    var lines = File.ReadAllLines("input.txt");
    foreach (var data in lines)
    {
       Console.WriteLine(GetLength(data));
    } 
}
long GetLength(string data)
{
     var start = -1;
        var ans = 0L;
        for (int i = 0; i < data.Length; i++)
        {
            if (start == -1) ans++;
            if (data[i] == '(') {start = i;ans--;}
            else if (data[i] == ')')
            {
                var dig = data.Substring(start+1, i - start-1).Split("x").Select(x => int.Parse(x)).ToArray();
                start = -1;
               
                var subLen = GetLength(data.Substring(i+1, dig[0]));
                i += dig[0];
                ans += subLen * dig[1];
            }
        }
    return ans;
}
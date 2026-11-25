Solve();
Solve(true);
void Solve(bool partTwo=false)
{
    var discs = Parse("input.txt");
    if (partTwo) discs.Add((11,0));
    var time =0;
    while (true)
    {
        bool good=true;
        for (int i=0; i<discs.Count; i++)
        {
            if ((discs[i].pos+time+i+1)%discs[i].size!=0)
            {
                good=false;
                break;
            }
        }
        if (good){Console.WriteLine(time); break;} 
        time++;
    }
}
List<(int size, int pos)> Parse(string input)
{
    var ans = new List<(int size, int pos)>();
    foreach( var line in File.ReadAllLines(input))
    {
        var split = line.Split(' ');
        ans.Add((int.Parse(split[3]), int.Parse(split[11].Substring(0, split[11].Length-1))));
    }
    return ans;
}
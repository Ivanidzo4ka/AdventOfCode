PartOne();
PartTwo();
void PartOne()
{
     var lines = File.ReadAllLines("input.txt");
     for(int i=0; i< lines[0].Length; i++)
     {
        var freq = new Dictionary<char, int>();
        foreach(var line in lines)
        {
            freq.TryGetValue(line[i], out var c);
            freq[line[i]] = c+1;
        }
        Console.Write(freq.OrderByDescending(x=>x.Value).First().Key);
     }
     Console.WriteLine();
}

void PartTwo()
{
     var lines = File.ReadAllLines("input.txt");
     for(int i=0; i< lines[0].Length; i++)
     {
        var freq = new Dictionary<char, int>();
        foreach(var line in lines)
        {
            freq.TryGetValue(line[i], out var c);
            freq[line[i]] = c+1;
        }
        Console.Write(freq.OrderBy(x=>x.Value).First().Key);
     }
     Console.WriteLine();
}
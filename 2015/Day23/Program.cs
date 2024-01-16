PartOne();

PartTwo();

void ProcessInstructions(IList<string> data, Dictionary<string, int> reg)
{
    var pos = 0;
    while (pos < data.Count)
    {
        var spl = data[pos].Split(" ");
        switch (spl[0])
        {
            case "hlf":
                reg[spl[1]] /= 2;
                pos++;
                break;
            case "tpl":
                reg[spl[1]] *= 3;
                pos++;
                break;
            case "inc":
                reg[spl[1]]++;
                pos++;
                break;
            case "jmp":
                pos += (int.Parse(spl[1]));
                break;
            case "jie":
                if (reg[spl[1].Trim(',')] % 2 == 0)
                    pos += (int.Parse(spl[2]));
                else
                    pos++;
                break;
            case "jio":
                if (reg[spl[1].Trim(',')] == 1)
                    pos += (int.Parse(spl[2]));
                else
                    pos++;
                break;
        }
    }
}



void PartOne()
{
    
    var data = Parse("input.txt");
    var reg = new Dictionary<string, int>
    {
        ["a"] = 0,
        ["b"] = 0,
    };
    ProcessInstructions(data, reg);
    Console.WriteLine(reg["b"]);
}
void PartTwo()
{
     var data = Parse("input.txt");
    var reg = new Dictionary<string, int>
    {
        ["a"] = 1,
        ["b"] = 0,
    };
    ProcessInstructions(data, reg);
    Console.WriteLine(reg["b"]);
}

IList<string> Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);

    return lines;
}
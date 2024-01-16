StepOne();
StepTwo();

void StepOne()
{
    var total = 0;
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var first = line.Substring(0, line.Length / 2).Select(x => x).ToHashSet();
        var second = line.Substring(line.Length / 2).Select(x => x).ToHashSet();

        foreach (var o in first)
        {
            if (second.Contains(o))
            {
                if (o <= 'Z')
                    total += (o - 'A' + 27);
                else
                    total += (o - 'a' + 1);
            }
        }
    }
    Console.WriteLine(total);
}

void StepTwo()
{
    var total = 0;
    foreach (var line in File.ReadAllLines("input.txt").Chunk(3))
    {
        var first = line[0].Select(x => x).ToHashSet();
        var second = line[1].Select(x => x).ToHashSet();
        var third = line[2].Select(x => x).ToHashSet();
        foreach (var o in first)
        {
            if (second.Contains(o) && third.Contains(o))
            {
                if (o <= 'Z')
                    total += (o - 'A' + 27);
                else
                    total += (o - 'a' + 1);
            }
        }
    }
    Console.WriteLine(total);
}
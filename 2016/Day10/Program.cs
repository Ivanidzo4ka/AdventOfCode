
PartOne();
PartTwo();
void PartOne()
{
    var bots = Parse("input.txt");
    var queue = new Queue<int>();
    for (int i = 0; i < bots.Length; i++)
    {
        if (bots[i] != null && bots[i].Chips[0] != -1 && bots[i].Chips[1] != -1)
        {
            queue.Enqueue(i);
        }
    }
    while (queue.Count != 0)
    {
        var bot = queue.Dequeue();
        if (bots[bot].Chips[0] == 17 && bots[bot].Chips[1] == 61 || bots[bot].Chips[1] == 17 && bots[bot].Chips[0] == 61)
        {
            Console.WriteLine(bot);
        }
        if (bots[bot].LowBot != -1)
        {
            bots[bots[bot].LowBot] = Add(bots[bots[bot].LowBot], Math.Min(bots[bot].Chips[0], bots[bot].Chips[1]));
            if (bots[bots[bot].LowBot].Chips[0] != -1 && bots[bots[bot].LowBot].Chips[1] != -1)
            {
                queue.Enqueue(bots[bot].LowBot);
            }
        }
        if (bots[bot].HighBot != -1)
        {
            bots[bots[bot].HighBot] = Add(bots[bots[bot].HighBot], Math.Max(bots[bot].Chips[0], bots[bot].Chips[1]));
            if (bots[bots[bot].HighBot].Chips[0] != -1 && bots[bots[bot].HighBot].Chips[1] != -1)
            {
                queue.Enqueue(bots[bot].HighBot);
            }
        }
        bots[bot].Chips = new int[] { -1, -1 };

    }

}

void PartTwo()
{
    var bots = Parse("input.txt");
    var queue = new Queue<int>();
    var next = new Queue<int>();
    for (int i = 0; i < bots.Length; i++)
    {
        if (bots[i] != null && bots[i].Chips[0] != -1 && bots[i].Chips[1] != -1)
        {
            queue.Enqueue(i);
        }
    }
    Dictionary<int, int> outputs = new Dictionary<int, int>();
    while (queue.Count != 0)
    {
        var bot = queue.Dequeue();

        if (bots[bot].LowBot != -1)
        {
            bots[bots[bot].LowBot] = Add(bots[bots[bot].LowBot], Math.Min(bots[bot].Chips[0], bots[bot].Chips[1]));
            if (bots[bots[bot].LowBot].Chips[0] != -1 && bots[bots[bot].LowBot].Chips[1] != -1)
            {
                queue.Enqueue(bots[bot].LowBot);
            }
        }
        else
        {
            outputs[bots[bot].LowOutput] = Math.Min(bots[bot].Chips[0], bots[bot].Chips[1]);
        }
        if (bots[bot].HighBot != -1)
        {
            bots[bots[bot].HighBot] = Add(bots[bots[bot].HighBot], Math.Max(bots[bot].Chips[0], bots[bot].Chips[1]));
            if (bots[bots[bot].HighBot].Chips[0] != -1 && bots[bots[bot].HighBot].Chips[1] != -1)
            {
                queue.Enqueue(bots[bot].HighBot);
            }
        }
        else
        {
            outputs[bots[bot].HighOutput] = Math.Max(bots[bot].Chips[0], bots[bot].Chips[1]);
        }
        bots[bot].Chips = new int[] { -1, -1 };
    }
    Console.WriteLine(outputs[0] * outputs[1] * outputs[2]);
}

Bot[] Parse(string file)
{
    var bots = new Bot[256];
    foreach (var line in File.ReadAllLines(file))
    {
        var split = line.Split(" ");
        //value 61 goes to bot 144
        if (line.StartsWith("value"))
        {
            var bot = int.Parse(split[5]);
            var value = int.Parse(split[1]);
            bots[bot] = Add(bots[bot], value);
        }
        //bot 119 gives low to bot 207 and high to bot 65
        else
        {
            var bot = int.Parse(split[1]);
            var lowBot = int.Parse(split[6]);
            var highBot = int.Parse(split[11]);
            if (bots[bot] == null) bots[bot] = new Bot() { Chips = new int[] { -1, -1 } };
            if (split[5] == "bot") { bots[bot].LowBot = lowBot; } else { bots[bot].LowOutput = lowBot; }
            if (split[10] == "bot") bots[bot].HighBot = highBot; else { bots[bot].HighOutput = highBot; }
        }
    }

    return bots;
}
Bot Add(Bot bot, int value)
{
    if (bot == null) bot = new Bot() { Chips = new int[] { value, -1 } };
    else

    {
        if (bot.Chips[0] == -1) bot.Chips[0] = value;
        else if (bot.Chips[1] == -1) bot.Chips[1] = value;
        else
        {
            throw new Exception("Bot has too many chips");
        }
    }
    return bot;
}
class Bot
{
    public int[] Chips = new int[2];
    public int HighBot = -1;
    public int HighOutput = -1;
    public int LowBot = -1;
    public int LowOutput = -1;
}


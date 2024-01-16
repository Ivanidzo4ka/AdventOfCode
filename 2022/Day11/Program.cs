StepOne();
StepTwo();
void StepOne()
{
    var monkeys = new List<Monkey>();

    foreach (var line in File.ReadAllLines("input.txt").Chunk(7))
    {
        monkeys.Add(new Monkey(line));
    }
    int turns = 20;
    for (int i = 0; i < turns; i++)
    {
        foreach (var monkey in monkeys)
            monkey.Action(monkeys, (x) => x / 3);
    }
    var pr = monkeys.Select(x => x.Inspections).OrderByDescending(x => x).Take(2).ToArray();
    Console.WriteLine(pr[0] * pr[1]);
}

void StepTwo()
{
    var monkeys = new List<Monkey>();

    foreach (var line in File.ReadAllLines("input.txt").Chunk(7))
    {
        monkeys.Add(new Monkey(line));
    }
    var divisible = 1;
    foreach (var monkey in monkeys)
        divisible *= monkey.Test;
    int turns = 10000;
    for (int i = 0; i < turns; i++)
    {
        foreach (var monkey in monkeys)
            monkey.Action(monkeys, (x) => x % divisible);
    }
    var pr = monkeys.Select(x => x.Inspections).OrderByDescending(x => x).Take(2).ToArray();
    Console.WriteLine(pr[0] * pr[1]);
}

public class Monkey
{
    public List<long> Items;
    public char Op;
    public string Value;
    public int Test;
    public int ThrowTrue;
    public int ThrowFalse;
    public long Inspections;
    public Monkey(string[] lines)
    {
        Items = lines[1].Substring("  Starting items: ".Length).Split(",").Select(x => long.Parse(x)).ToList();
        Op = lines[2]["Operation: new = old ".Length + 2];
        Value = lines[2].Substring("Operation: new = old ".Length + 3);
        Test = int.Parse(lines[3].Substring("  Test: divisible by ".Length));
        ThrowTrue = int.Parse(lines[4].Substring("    If true: throw to monkey ".Length));
        ThrowFalse = int.Parse(lines[5].Substring("    If false: throw to monkey ".Length));
        Inspections = 0;
    }

    public void Action(List<Monkey> monkeys, Func<long, long> reduce)
    {
        foreach (var c in Items)
        {
            long item = c;
            long v = 0;
            if (Value == " old")
                v = item;
            else
                v = int.Parse(Value);
            if (Op == '+')
                item += v;
            else
                item *= v;
            item = reduce(item);
            if (item % Test == 0)
            {
                monkeys[ThrowTrue].Items.Add(item);
            }
            else
                monkeys[ThrowFalse].Items.Add(item);
            Inspections++;
        }
        Items.Clear();
    }

}
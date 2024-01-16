//StepOne();
StepTwo();

void StepOne()
{
    var root = new LL() { Index = -1 };
    var first = root;
    var last = root;
    int count = 0;
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var next = new LL() { Value = int.Parse(line), Index = count++ };
        last.Right = next;
        next.Left = last;
        last = next;
    }
    last.Right = first.Right;
    first = first.Right;
    first.Left = last;
    root = last.Right;
    LL start = null;
    for (int i = 0; i < count; i++)
    {
        start = root;
        while (start.Index != i)
            start = start.Left;
        var val = start.Value;
        if (val < 0)
        {
            while (val != 0)
            {
                var t = start.Value;
                start.Value = start.Left.Value;
                start.Left.Value = t;
                t = start.Index;
                start.Index = start.Left.Index;
                start.Left.Index = (int)t;
                start = start.Left;
                val++;
            }
        }
        if (val > 0)
        {
            while (val != 0)
            {
                var t = start.Value;
                start.Value = start.Right.Value;
                start.Right.Value = t;
                t = start.Index;
                start.Index = start.Right.Index;
                start.Right.Index = (int)t;
                start = start.Right;
                val--;
            }
        }
    }
    start = root;
    while (start.Value != 0)
        start = start.Right;
    long ans = 0;
    for (int i = 0; i <= 3000; i++)
    {

        if (i != 0 && i % 1000 == 0)
            ans += start.Value;
        start = start.Right;

    }
    Console.WriteLine(ans);
}

void StepTwo()
{
    var root = new LL() { Index = -1 };
    var first = root;
    var last = root;
    int count = 0;
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var next = new LL() { Value = long.Parse(line) * 811589153, Index = count++ };
        last.Right = next;
        next.Left = last;
        last = next;
    }
    last.Right = first.Right;
    first = first.Right;
    first.Left = last;
    root = last.Right;
    LL start = null;
    for (int repeat = 0; repeat < 10; repeat++)
    {
        for (int i = 0; i < count; i++)
        {
            start = root;
            while (start.Index != i)
                start = start.Left;
            var val = start.Value;

            val %= (count-1);
            if (val < 0)
            {
                while (val != 0)
                {
                    var t = start.Value;
                    start.Value = start.Left.Value;
                    start.Left.Value = t;
                    t = start.Index;
                    start.Index = start.Left.Index;
                    start.Left.Index = (int)t;
                    start = start.Left;
                    val++;
                }
            }
            if (val > 0)
            {
                while (val != 0)
                {
                    var t = start.Value;
                    start.Value = start.Right.Value;
                    start.Right.Value = t;
                    t = start.Index;
                    start.Index = start.Right.Index;
                    start.Right.Index = (int)t;
                    start = start.Right;
                    val--;
                }
            }

        }
    }
    start = root;
    while (start.Value != 0)
        start = start.Right;
    long ans = 0;
    for (int i = 0; i <= 3000; i++)
    {

        if (i != 0 && i % 1000 == 0)
            ans += start.Value;
        start = start.Right;

    }
    Console.WriteLine(ans);
}
void Print(LL root, int count)
{
    var l = new List<long>();
    for (int i = 0; i < count; i++)
    {
        l.Add(root.Value);
        root = root.Right;
    }
    Console.WriteLine(String.Join(",", l.Select(x => x)));
}




public class LL
{
    public LL Left;
    public LL Right;

    public long Value;
    public int Index;
}
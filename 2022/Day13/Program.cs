StepOne();
StepTwo();
void StepOne()
{
    var index = 1;
    var ans = 0;
    var comparer =new  LintComparer() ;
    foreach (var line in File.ReadAllLines("input.txt").Chunk(3))
    {
        var p = 0;
        var a = new LInt(line[0], ref p);
        p = 0;
        var b = new LInt(line[1], ref p);
        if (comparer.Compare(a, b) == -1)
            ans += index;
        index++;
    }
    Console.WriteLine(ans);
}

void StepTwo()
{
    var ll = new List<LInt>();
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        if (string.IsNullOrEmpty(line))
            continue;
        var pos = 0;
        ll.Add(new LInt(line, ref pos));
    }
    ll.Add(new LInt() { Interesting = true, List = new List<LInt>() { new LInt() { Single = true, Value = 6 } } });
    ll.Add(new LInt() { Interesting = true, List = new List<LInt>() { new LInt() { Single = true, Value = 2 } } });
    ll.Sort(new LintComparer());
    var ans = 1;
    for (int i = 0; i < ll.Count; i++)
        if (ll[i].Interesting)
            ans *= (i + 1);
    Console.WriteLine(ans);
}


public class LintComparer : IComparer<LInt>
{
    public int Compare(LInt a, LInt b)
    {
        if (a.Single && b.Single)
        {
            if (a.Value == b.Value) return 0;
            return a.Value < b.Value ? -1 : 1;
        }
        if (!a.Single && !b.Single)
        {
            for (int i = 0; i < a.List.Count; i++)
            {
                if (b.List.Count == i)
                    return 1;
                var comp = Compare(a.List[i], b.List[i]);
                if (comp == 1)
                    return 1;
                if (comp == -1)
                    return -1;
            }
            return a.List.Count == b.List.Count ? 0 : -1;
        }
        else
        {
            if (a.Single)
            {
                var fakea = new LInt() { List = new List<LInt>() { a }, Single = false };
                return Compare(fakea, b);
            }
            else
            {
                var fakeb = new LInt() { List = new List<LInt>() { b }, Single = false };
                return Compare(a, fakeb);
            }
        }

    }
}
public class LInt
{
    public int Value;
    public bool Single;
    public List<LInt> List;
    public bool Interesting;
    public LInt()
    {

    }
    public LInt(string str, ref int pos)
    {
        if (str[pos] == '[')
        {

            Single = false;
            List = new List<LInt>();
            do
            {
                pos++;
                List.Add(new LInt(str, ref pos));

            } while (pos < str.Length && str[pos] == ',');
            pos++;
        }
        else
        {
            Single = true;
            var next = pos;
            while (next < str.Length && !(str[next] == ',' || str[next] == ']'))
                next++;
            if (next != pos)
                Value = int.Parse(str.Substring(pos, next - pos));
            pos = next;

        }
    }
    public override string ToString()
    {
        if (Single)
            return Value.ToString();
        else 
            return "["+String.Join(",", List)+"]";
    }
}
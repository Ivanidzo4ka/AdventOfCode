StepOne();
StepTwo(3_221_245_824_363);

void StepOne()
{
    var ops = new Dictionary<string, Operation>();
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var op = new Operation(line);
        ops.Add(op.Name, op);
    }
    var ans = ops["root"].GetValue(ops);
    Console.WriteLine(ans);
}
void StepTwo(long val)
{
    var ops = new Dictionary<string, Operation>();
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        var op = new Operation(line);
        ops.Add(op.Name, op);
    }
    ops["humn"].Value = val;
    var left = ops["jntz"].GetValue(ops);
    ops["humn"].Value = val;
    var right = ops["prrg"].GetValue(ops);
    Console.WriteLine(right);
    Console.WriteLine(left);
    if (right == left)
    { Console.WriteLine(val); }
    else
    {
        Console.WriteLine(right > left ? "too much" : "less");
    }
   

}

public class Operation
{
    public string Name;
    public long? Value;
    public string MonkeyOne;
    public string MonkeyTwo;
    public char Op;
    public Operation(string str)
    {
        Name = str.Substring(0, 4);
        var rest = str.Substring(6);
        if (rest.Length == 11)
        {
            MonkeyOne = rest.Substring(0, 4);
            MonkeyTwo = rest.Substring(7, 4);
            Op = rest[5];
        }
        else
        {
            Op = ' ';
            Value = long.Parse(rest);
        }
    }

    public long GetValue(Dictionary<string, Operation> ops)
    {
        if (Value.HasValue) return Value.Value;
        else
        {
            var a = ops[MonkeyOne].GetValue(ops);
            var b = ops[MonkeyTwo].GetValue(ops);
            checked
            {
                Value = Op switch
                {
                    '+' => a + b,
                    '-' => a - b,
                    '*' => a * b,
                    '/' => a / b,
                };
            }
            return Value.Value;
        }
    }
}
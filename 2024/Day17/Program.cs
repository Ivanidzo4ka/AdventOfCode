

long second = long.MaxValue;

PartOne(true);
PartOne(false);
//PartTwo(true);
PartTwo(false);
void PartOne(bool test = false)
{
    var data = ReadData(test);

    var ans = 0L;
    if (test)
        Console.Write("Test:");
    long regA = data.A;
    long regB = data.B;
    long regC = data.C;
    var output = new List<long>();
    for (int i = 0; i < data.program.Count / 2; i++)
    {
        var opcode = data.program[i * 2];
        var operand = data.program[1 + i * 2];
        switch (opcode)
        {
            case 0: regA /= (1 << (int)ComboOp(operand, regA, regB, regC)); break;
            case 1: regB ^= operand; break;
            case 2: regB = ComboOp(operand, regA, regB, regC) % 8; break;
            case 3:
                if (regA == 0) continue;
                i = (operand / 2) - 1;
                break;
            case 4:
                regB ^= regC;
                break;
            case 5: output.Add(ComboOp(operand, regA, regB, regC) % 8); break;
            case 6:
                regB = regA / (1 << (int)ComboOp(operand, regA, regB, regC));
                break;
            case 7:
                regC = regA / (1 << (int)ComboOp(operand, regA, regB, regC));
                break;
        }
    }
    Console.WriteLine(string.Join(",", output));
}
long ComboOp(int op, long regA, long regB, long regC)
{
    switch (op)
    {
        case 0:
        case 1:
        case 2:
        case 3:
            return op;
        case 4:
            return regA;
        case 5:
            return regB;
        case 6:
            return regC;
        case 7:
            throw new NotImplementedException();
    }
    throw new NotImplementedException();
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    bool found = false;
    for (int i = 0; i < 8; i++)
    {
        var unknown = Enumerable.Repeat('X', 64).ToArray();
        Deconstruct(data.program, i, 0, 0, unknown);
    }

    Console.WriteLine(second);
}
void Deconstruct(List<int> program, int b, int pos, int shift, char[] ans)
{
    var curans = new char[ans.Length];
    Array.Copy(ans, curans, ans.Length);
    if (pos == program.Count)
    {
        var curAns = 0L;
        var mul = 1L;
        foreach (var l in ans)
        {
            if (l == '1') curAns += mul;
            mul *= 2;
        }
        second = Math.Min(curAns, second);
        //Console.WriteLine(string.Join("", ans));
        return;
    }
    var tb = b;
    for (int i = 0; i < 3; i++)
    {
        var exp = tb % 2 == 0 ? '0' : '1';
        if (ans[shift + i] == 'X')
            ans[shift + i] = exp;
        else if (ans[shift + i] != exp)
        {
            Array.Copy(curans, ans, ans.Length);
            return;
        }
        tb /= 2;
    }


    var cleft = program[pos] ^ 5 ^ 3 ^ b;
    var cshift = shift + (b ^ 3);
    for (int i = 0; i < 3; i++)
    {
        var exp = cleft % 2 == 0 ? '0' : '1';
        if (ans[cshift + i] == 'X') ans[cshift + i] = exp;
        else if (ans[cshift + i] != exp)
        {
            Array.Copy(curans, ans, ans.Length);
            return;
        }
        cleft /= 2;
    }
    for (int i = 0; i < 8; i++)
        Deconstruct(program, i, pos + 1, shift + 3, ans);
    Array.Copy(curans, ans, ans.Length);

}

(long A, long B, long C, List<int> program) ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var a = long.Parse(lines[0].Substring("Register A: ".Length));
    var b = long.Parse(lines[1].Substring("Register A: ".Length));
    var c = long.Parse(lines[2].Substring("Register A: ".Length));
    var program = lines[4].Substring("Program: ".Length).Split(",").Select(x => int.Parse(x)).ToList();
    return (a, b, c, program);
}
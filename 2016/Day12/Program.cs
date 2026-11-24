
PartOne();
PartTwo();
void PartOne()
{
    var instructions = Parse("input.txt");
    var registers = new Dictionary<char, int>();
    registers['a'] = 0;
    registers['b'] = 0;
    registers['c'] = 0;
    registers['d'] = 0;
    int pos = 0;
    while (pos < instructions.Length)
    {
        instructions[pos].Act(registers, ref pos);
    }
    Console.WriteLine(registers['a']);
}

void PartTwo()
{
    var instructions = Parse("input.txt");
    var registers = new Dictionary<char, int>();
    registers['a'] = 0;
    registers['b'] = 0;
    registers['c'] = 1;
    registers['d'] = 0;
    int pos = 0;
    while (pos < instructions.Length)
    {
        instructions[pos].Act(registers, ref pos);
    }
    Console.WriteLine(registers['a']);
}


IInstruction[] Parse(string input)
{
    var instructions = new List<IInstruction>();
    foreach(var line in File.ReadAllLines(input))
    {
        var split = line.Split(' ');
        switch (split[0])
        {
            case "cpy":
                if (char.IsLetter(split[1][0]))
                {
                    instructions.Add(new CopyReg(split[1][0], split[2][0]));
                }
                else
                {
                    instructions.Add(new CopyVal(int.Parse(split[1]), split[2][0]));
                }
                break;
            case "inc":
                instructions.Add(new Inc(split[1][0]));
                break;
            case "dec":
                instructions.Add(new Dec(split[1][0]));
                break;
            case "jnz":
                instructions.Add(new Jmp(split[1][0], int.Parse(split[2])));
                break;

        }
    }
    return instructions.ToArray();
}

interface IInstruction
{
    void Act(Dictionary<char, int> registers, ref int pos);
}
public class CopyVal:IInstruction
{
    public int A;
    public char Reg;
    public CopyVal(int a, char reg)
    {
        A=a;
        Reg=reg;
    }
    public void Act(Dictionary<char, int> registers, ref int  pos)
    {
        registers[Reg] = A;
        pos++;
    }
}
public class CopyReg:IInstruction
{
    public char A;
    public char Reg;
    public CopyReg(char a, char reg)
    {
        A=a;
        Reg=reg;
    }
    public void Act(Dictionary<char, int> registers, ref int  pos)
    {
        registers[Reg] = registers[A];
        pos++;
    }
}
public class Inc:IInstruction
{
    public char Reg;
    public Inc(char reg)
    {
        Reg=reg;
    }
    public void Act(Dictionary<char, int> registers, ref int  pos)
    {
        registers[Reg]++;
        pos++;
    }
}
public class Dec:IInstruction
{
    public char Reg;
    public Dec(char reg)
    {
        Reg=reg;
    }
    public void Act(Dictionary<char, int> registers, ref int  pos)
    {
        registers[Reg]--;
        pos++;
    }
}

public class Jmp:IInstruction
{
    public char A;
    public int B;
    public Jmp(char a,int b)
    {
        A=a;
        B=b;
    }
    public void Act(Dictionary<char, int> registers, ref int  pos)
    {
        if (!registers.ContainsKey(A) || registers[A] != 0)
        {
            pos += B;
        }
        else
        {
            pos++;
        }
    }
}

using System.Text;
//StepOne();
StepTwo();
void StepOne()
{
    var index = 0;
    var state = new State();
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        for (int j = 0; j < line.Length; j++)
        {
            if (line[j] == '#')
                state.Add(index, j);
        }
        index++;
    }
    for (int i = 0; i < 10; i++)
    {
        state.Move();
        //state.Print();
    }
    var maxx = int.MinValue;
    var maxy = int.MinValue;
    var minx = int.MaxValue;
    var miny = int.MaxValue;
    foreach (var elf in state.Current)
    {
        maxx = Math.Max(maxx, elf.X);
        maxy = Math.Max(maxy, elf.Y);
        minx = Math.Min(minx, elf.X);
        miny = Math.Min(miny, elf.Y);
    }

    var space = (maxx - minx + 1) * (maxy - miny + 1);
    Console.WriteLine(space - state.Current.Count);

}

void StepTwo()
{
    var index = 0;
    var state = new State();
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        for (int j = 0; j < line.Length; j++)
        {
            if (line[j] == '#')
                state.Add(index, j);
        }
        index++;
    }
    index = 1;
    while (state.Move())
    {
       //state.Print();
        index++;

    }
    Console.WriteLine(index);
}
public class State
{
    private (int x, int y)[] directions = new (int x, int y)[12]{
                    (-1,0), (-1,1),(-1,-1), // N
                    (1,0),(1,1),(1,-1),//S 
                    (0,-1), (1,-1), (-1,-1), //W
                    (0,1), (1,1), (-1,1), //E
                    };
    public List<Elf> Current = new List<Elf>();
    public int Starting = 0;
    public HashSet<(int x, int y)> Positions = new HashSet<(int x, int y)>();
    public void Add(int x, int y)
    {
        Current.Add(new Elf() { X = x, Y = y });
        Positions.Add((x, y));
    }

    public bool Move()
    {
        var proposed = new Dictionary<(int x, int y), List<int>>();
        for (int elfI = 0; elfI < Current.Count; elfI++)
        {
            var elf = Current[elfI];
            var count = 0;
            for (int i = 0; i < directions.Length; i++)
                if (Positions.Contains((elf.X + directions[i].x, elf.Y + directions[i].y)))
                {
                    count++;
                }
            if (count != 0)
            {
                var moveDir = -1;
                for (int i = 0; i < 4; i++)
                {
                    count = 0;
                    for (int x = 0; x < 3; x++)
                    {
                        var newx = elf.X + directions[((Starting + i) * 3 + x) % 12].x;
                        var newy = elf.Y + directions[((Starting + i) * 3 + x) % 12].y;
                        if (Positions.Contains((newx, newy)))
                        {
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        moveDir = (Starting + i) % 4;
                        break;
                    }
                }
                if (moveDir != -1)
                {
                    var px = elf.X + directions[moveDir * 3].x;
                    var py = elf.Y + directions[moveDir * 3].y;
                    if (!proposed.ContainsKey((px, py)))
                    {
                        proposed[(px, py)] = new List<int>();
                    }
                    proposed[(px, py)].Add(elfI);
                }
            }
        }
        if (proposed.Count == 0)
            return false;
        foreach (var moves in proposed)
        {
            if (moves.Value.Count == 1)
            {
                var elf = Current[moves.Value[0]];
                Positions.Remove((elf.X, elf.Y));
                elf.X = moves.Key.x;
                elf.Y = moves.Key.y;
                Positions.Add((elf.X, elf.Y));
            }
        }
        Starting++;
        Starting %= 4;
        return true;
    }

    public void Print()
    {
        for (int i = -5; i < 20; i++)
        {
            var sb = new StringBuilder();
            for (int j = -5; j < 20; j++)
                if (Positions.Contains((i, j)))
                    sb.Append('#');
                else
                    sb.Append('.');
            sb.Append("    ");
            sb.Append(i);
            Console.WriteLine(sb.ToString());
        }
        Console.WriteLine();
    }
}

public class Elf
{
    public int X;
    public int Y;


}
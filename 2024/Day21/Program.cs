
using System.Text;

var dir = new (int x, int y)[] { (0, 1), (1, 0), (-1, 0), (0, -1) };
var keys = new Dictionary<char, (int x, int y)>
{
    ['<'] = (1, 0),
    ['v'] = (1, 1),
    ['>'] = (1, 2),
    ['^'] = (0, 1),
    ['A'] = (0, 2)
};
var memo = new Dictionary<(char a, char b, int layer), long>();
CountWays(robots: 2);
CountWays(robots: 25);


void CountWays(bool test = false, int robots = 2)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    var map = new Dictionary<int, List<string>>()
    {
        [579] = new List<string>()
        {
             "<^^A<^A>>AvvvA",
             "^<^A<^A>>AvvvA",
             "^^<A<^A>>AvvvA",
             "<^^A^<A>>AvvvA",
             "^<^A^<A>>AvvvA",
             "^^<A^<A>>AvvvA",
        },


        [593] = new List<string>()
        {
              "<^^A>^AvvAvA",
              "^<^A>^AvvAvA",
              "^^<A>^AvvAvA",

              "<^^A^>AvvAvA",
              "^<^A^>AvvAvA",
              "^^<A^>AvvAvA",
        },
        [169] = new List<string>()
        {
            "<^<A>>^A^AvvvA",
            "^<<A>>^A^AvvvA",

            "<^<A>^>A^AvvvA",
            "^<<A>^>A^AvvvA",

            "<^<A^>>A^AvvvA",
            "^<<A^>>A^AvvvA",
        },
        [582] = new List<string>()
        {
             "^^<A^AvvA>vA",
             "^<^A^AvvA>vA",
             "<^^A^AvvA>vA",

             "^^<A^AvvAv>A",
             "^<^A^AvvAv>A",
             "<^^A^AvvAv>A",

        },
        [540] = new List<string>()
        {
            "^^<A<A>vvA>A",
            "^<^A<A>vvA>A",
            "<^^A<A>vvA>A",

            "^^<A<Av>vA>A",
            "^<^A<Av>vA>A",
            "<^^A<Av>vA>A",
        }
    };
    foreach (var kpv in map)
    {
        var min = long.MaxValue;
        foreach (var val in kpv.Value)
        {
            var line = $"A{val}";
            var curMin = 0L;
            for (int i = 1; i < line.Length; i++)
            {
                curMin += Count(line[i - 1], line[i], robots - 1);
            }

            min = Math.Min(curMin, min);
        }
        ans += kpv.Key * min;
    }


    Console.WriteLine(ans);
}
long Count(char a, char b, int layer)
{
    if (layer == 0)
    {
        return Math.Abs(keys[a].x - keys[b].x) + Math.Abs(keys[a].y - keys[b].y) + 1;
    }
    else
    {
        if (memo.TryGetValue((a, b, layer), out var ans))
        {
            return ans;
        }
        switch (a)
        {
            case 'A':
                switch (b)
                {
                    // A< -> 
                    case '<':
                        ans = Count('A', 'v', layer - 1) + Count('v', '<', layer - 1) + Count('<', '<', layer - 1) + Count('<', 'A', layer - 1);
                        ans = Math.Min(ans, Count('A', '<', layer - 1) + Count('<', 'v', layer - 1) + Count('v', '<', layer - 1) + Count('<', 'A', layer - 1));
                        break;
                    //A^-> <A 
                    case '^':
                        ans = Count('A', '<', layer - 1) + Count('<', 'A', layer - 1);
                        break;
                    //A> -> vA
                    case '>':
                        ans = Count('A', 'v', layer - 1) + Count('v', 'A', layer - 1);
                        break;
                    //Av -> <vA
                    case 'v':
                        ans = Count('A', '<', layer - 1) + Count('<', 'v', layer - 1) + Count('v', 'A', layer - 1);
                        ans = Math.Min(ans, Count('A', 'v', layer - 1) + Count('v', '<', layer - 1) + Count('<', 'A', layer - 1));
                        break;
                    case 'A':
                        return 1;
                }
                break;
            case '^':
                switch (b)
                {
                    // ^< -> v<A
                    case '<':
                        ans = Count('A', 'v', layer - 1) + Count('v', '<', layer - 1) + Count('<', 'A', layer - 1);
                        break;
                    //^^->  
                    case '^':
                        return 1;
                    //^> -> v>A
                    case '>':
                        ans = Count('A', 'v', layer - 1) + Count('v', '>', layer - 1) + Count('>', 'A', layer - 1);
                        ans = Math.Min(ans, Count('A', '>', layer - 1) + Count('>', 'v', layer - 1) + Count('v', 'A', layer - 1));
                        break;
                    //^v -> vA
                    case 'v':
                        ans = Count('A', 'v', layer - 1) + Count('v', 'A', layer - 1);
                        break;
                    //^A -> >A
                    case 'A':
                        ans = Count('A', '>', layer - 1) + Count('>', 'A', layer - 1);
                        break;
                }
                break;
            case '>':
                switch (b)
                {
                    // >< -> <<A
                    case '<':
                        ans = Count('A', '<', layer - 1) + Count('<', '<', layer - 1) + Count('<', 'A', layer - 1);
                        break;
                    //>^-> <^A  
                    case '^':
                        ans = Count('A', '<', layer - 1) + Count('<', '^', layer - 1) + Count('^', 'A', layer - 1);
                        ans = Math.Min(ans, Count('A', '^', layer - 1) + Count('^', '<', layer - 1) + Count('<', 'A', layer - 1));
                        break;
                    //>> -> 
                    case '>':
                        return 1;
                    //>v -> <A
                    case 'v':
                        ans = Count('A', '<', layer - 1) + Count('<', 'A', layer - 1);
                        break;
                    //>A -> ^A
                    case 'A':
                        ans = Count('A', '^', layer - 1) + Count('^', 'A', layer - 1);
                        break;
                }
                break;
            case '<':
                switch (b)
                {
                    // << -> 
                    case '<':
                        return 1;
                    //<^-> >^A  
                    case '^':
                        ans = Count('A', '>', layer - 1) + Count('>', '^', layer - 1) + Count('^', 'A', layer - 1);

                        break;
                    //<> -> >>A 
                    case '>':
                        ans = Count('A', '>', layer - 1) + Count('>', '>', layer - 1) + Count('>', 'A', layer - 1);
                        break;
                    //<v -> >A
                    case 'v':
                        ans = Count('A', '>', layer - 1) + Count('>', 'A', layer - 1);
                        break;
                    //<A -> >>^A
                    case 'A':
                        ans = Count('A', '>', layer - 1) + Count('>', '>', layer - 1) + Count('>', '^', layer - 1) + Count('^', 'A', layer - 1);
                        ans = Math.Min(ans, Count('A', '>', layer - 1) + Count('>', '^', layer - 1) + Count('^', '>', layer - 1) + Count('>', 'A', layer - 1));
                        break;
                }
                break;
            case 'v':
                switch (b)
                {
                    // v< -> <A
                    case '<':
                        return Count('A', '<', layer - 1) + Count('<', 'A', layer - 1);
                    //v^-> ^A  
                    case '^':
                        ans = Count('A', '^', layer - 1) + Count('^', 'A', layer - 1);
                        break;
                    //v> -> >A 
                    case '>':
                        ans = Count('A', '>', layer - 1) + Count('>', 'A', layer - 1);
                        break;
                    //vv -> >A
                    case 'v':
                        return 1;
                    //vA -> >^A
                    case 'A':
                        ans = Count('A', '>', layer - 1) + Count('>', '^', layer - 1) + Count('^', 'A', layer - 1);
                        ans = Math.Min(ans, Count('A', '^', layer - 1) + Count('^', '>', layer - 1) + Count('>', 'A', layer - 1));
                        break;
                }
                break;
        }
        memo[(a, b, layer)] = ans;
        return ans;
    }
}


string[] ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    return lines;
}